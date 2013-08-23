define(function (require) {
    var system = require('durandal/system');

    // service name is route to the Web API controller
    var serviceName = 'breeze/todo';
    
    // manager is the service gateway and cache holder
    var manager = new breeze.EntityManager(serviceName);
    // manager.saveOptions = new breeze.SaveOptions({ allowConcurrentSaves: true });

    var hub = $.connection.todo;


    // define the viewmodel
    var vm = {
        todos: ko.observableArray(),
        includeDone: ko.observable(false),
        save: saveChanges,
        show: ko.observable(false),
        
        activate: function() {
            return getTodos();
        },
        

        update : updateItem
    };

    hub.client.updateItems = function (entities) {
        $.each(entities, function () {
            $.each(this, function (key, object) {
                vm.update(object.Id, object.Description, object.IsDone);
            });
        });
    };


    $.connection.hub.start();


    // start fetching Todos
    getTodos();

    // re-query when "includeDone" checkbox changes
    vm.includeDone.subscribe(getTodos);


    // bind view to the viewmodel
    // ko.applyBindings(vm);

    //#region private functions

    function updateItem(id, description, isDone) {
        
        var localEntity = manager.getEntityByKey("BreezeSampleTodoItem:#DurandalBreeze.Models", id);
        if (localEntity) {
            //localEntity.Description = description + " Ooops";
            localEntity.IsDone(isDone);
        }
        //var oldItem = ko.utils.arrayFirst(vm.todos(), function(i) {
        //    alert(i.Id);
        //    return i.Id === id;
        //});
        //if (oldItem) {
        //    oldItem.Description = description;
        //    oldItem.IsDone = isDone;
        //}
    }

    // get Todos asynchronously
    // returning a promise to wait for     
    function getTodos() {

        // logger.info("querying Todos");
        system.log("querying Todos");
        toastr.info("querying Todos");

        var query = breeze.EntityQuery.from("Todos");

        if (!vm.includeDone()) {
            query = query.where("IsDone", "==", false);
        }

        return manager
            .executeQuery(query)
            .then(querySucceeded)
            .fail(queryFailed);

        // reload vm.todos with the results 
        function querySucceeded(data) {
            system.log('queried todos');
            toastr.info('queried todos');
            //logger.success("queried Todos");
            vm.todos(data.results);
            vm.show(true); // show the view
        }
    };

    function queryFailed(error) {
        system.log("Query failed: " + error.message);
        toastr.error("Query failed: " + error.message);
        // logger.error("Query failed: " + error.message);
    }

    function saveChanges() {
        return manager.saveChanges()
            .then(function () {
                system.log("changes saved");
                toastr.success("changes saved");

                // logger.success("changes saved");
            })
            .fail(saveFailed);
    }

    function saveFailed(error) {
        system.log("Save failed: " + error.message);
        toastr.error("Save failed: " + error.message);
        // logger.error("Save failed: " + error.message);
    }
    //#endregion

    return vm;
});