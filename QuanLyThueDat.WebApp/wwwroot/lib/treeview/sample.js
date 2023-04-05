function CreateTreeView(data, treeview, selected) {
    // Hiển thị danh sách
    const selectedEmployeesList = $(selected).dxList({
        width: 400,
        height: 200,
        showScrollbar: 'always',
        noDatatext: "Chưa chọn đại biểu nào",
        itemTemplate(item) {
            return `<div>${item.text}</div>`;
        },
    }).dxList('instance');

    $(treeview).dxTreeView({
        items: data,
        //width: 340,
        //height: 320,
        showCheckBoxesMode: 'normal',
        searchMode: "contains",
        searchExpr: null,
        searchEditorOptions: {},
        noDatatext: "không có kết quả hợp lệ",
        searchEnabled: true,
        selectByClick: true,
        selectNodesRecursive: true,
        selectionMode: 'multiple',
        onSelectionChanged(e) {
            syncSelection(e.component);
        },
        onContentReady(e) {
            syncSelection(e.component);
        },
        itemTemplate(item) {
            return `<div>${item.text}</div>`;
        },
    }).dxTreeView('instance');


    function syncSelection(treeViewInstance) {
        const selectedNodes = treeViewInstance.getSelectedNodes()
            .map((node) => node.itemData);
        //selectedEmployeesList.option('items', selectedEmployees);

        // Gán listId vào data-id và chỉ lọc ra những thành phần con có id > 0
        var selectedChildren = [];
        var listId = [];
        for (var i = 0; i < selectedNodes.length; i++) {
            if (selectedNodes[i].id > 0) {
                selectedChildren.push(selectedNodes[i]);
                listId.push(selectedNodes[i].id);
            }
        }
        selectedEmployeesList.option('items', selectedChildren);

        $(selected).attr("data-id", listId.join());
    }
};

//$(document).ready(function () {
//    var treeview = $('#dxtreeview');
//    var selected = $('#selected-employees');
//    var data = [{
//        id: -1,
//        text: 'UBND tỉnh Nghệ An',
//        prefix: 'Dr.',
//        position: '',
//        expanded: true,
//        items: [{
//            id: -2,
//            text: 'Samantha Bright',
//            prefix: 'Dr.',
//            position: 'COO',
//            expanded: true,
//            items: [{
//                id: 3,
//                text: 'Kevin Carter',
//                prefix: 'Mr.',
//                position: 'Shipping Manager',
//            }, {
//                id: 14,
//                text: 'Victor Norris',
//                prefix: 'Mr.',
//                selected: true,
//                position: 'Shipping Assistant',
//            }],
//        }, {
//            id: 4,
//            text: 'Brett Wade',
//            prefix: 'Mr.',
//            position: 'IT Manager',
//            expanded: true,
//            items: [{
//                id: 5,
//                text: 'Amelia Harper',
//                prefix: 'Mrs.',
//                position: 'Network Admin',
//            }, {
//                id: 6,
//                text: 'Wally Hobbs',
//                prefix: 'Mr.',
//                position: 'Programmer',
//            }, {
//                id: 7,
//                text: 'Brad Jameson',
//                prefix: 'Mr.',
//                position: 'Programmer',
//            }, {
//                id: 8,
//                text: 'Violet Bailey',
//                prefix: 'Ms.',
//                position: 'Jr Graphic Designer',
//            }],
//        }, {
//            id: 9,
//            text: 'Barb Banks',
//            prefix: 'Mrs.',
//            position: 'Support Manager',
//            expanded: true,
//            items: [{
//                id: 10,
//                text: 'Kelly Rodriguez',
//                prefix: 'Ms.',
//                position: 'Support Assistant',
//            }, {
//                id: 11,
//                text: 'James Anderson',
//                prefix: 'Mr.',
//                position: 'Support Assistant',
//            }],
//        }],
//    }]

//    CreateTreeView(data, treeview, selected);
//});
