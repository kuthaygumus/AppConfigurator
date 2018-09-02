var app = new Vue({
    el: '#app',
    data: function () {
        return {
            configItems: [],
            Types: [
                { text: 'Int', value: 1 },
                { text: 'Bool', value: 2 },
                { text: 'String', value: 3 }
            ]
        }
    },
    created: function () {
        var self = this;
        fetch("/api/config").then((response) => {
            return response.json();
        }).then((data) => {
            self.updateItems(data);
        });
    },
    methods: {
        updateItems: function (items) {
            this.configItems = items;
        },
        addNewLine: function () {
            this.configItems.push({});
        },
        deleteItem: function (item) {
            var self = this;
            fetch(`/api/config/${item.ApplicationName}:${item.Name}`,
                    { method: 'DELETE' })
                .then((response) => response.json())
                .then((json) => json ? self.configItems.pop(item) : 0);
        },
        save: function () {
            for (var idx in this.configItems) {
                var config = this.configItems[idx];
                fetch(`/api/config`, {
                    method: "POST",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(config)
                });
            }
        }
    }
})