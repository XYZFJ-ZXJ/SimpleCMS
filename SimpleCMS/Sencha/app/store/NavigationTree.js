//116页底部删除了children配置项内的子节点，在118页底部又添加了一个空白页节点,500视图部分是120页添加的，登录视图是120页底部添加的,修改密码视图部分是122页添加的
Ext.define('SimpleCMS.store.NavigationTree', {
    extend: 'Ext.data.TreeStore',

    storeId: 'NavigationTree',

    fields: [{
        name: 'text'
    }],

    root: {
        expanded: true,
        children: [
            {
                text: '空白页',
                viewType: 'pageblank',
                leaf: true,
                visible: false
            },
            {
                text: '500视图',
                viewType: 'page500',
                leaf: true,
                visible: false
            },
            {
                text: '登录视图',
                viewType: 'login',
                leaf: true,
                visible: false
            },
            {
                text: '修改密码视图',
                viewType: 'passwordreset',
                leaf: true,
                visible: false
            }
        ]
    }
});
