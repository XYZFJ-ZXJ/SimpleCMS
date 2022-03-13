//'SimpleCMS.view.main.MainController','SimpleCMS.view.main.MainModel','SimpleCMS.view.main.MainContainerWrap'是115页底部加入的内容.
//116页修改了logo代码的html配置项的内容.显示用户名组件text配置项修改为bind配置项（116），'SimpleCMS.locale.Locale','SimpleCMS.util.Url'配置项不需要加入本类requires配置项中，
//因为本地化类是静态类，已经在Application.js类中配置过了.'SimpleCMS.view.authentication.*','SimpleCMS.view.pages.*'是118页上部添加的.
Ext.define('SimpleCMS.view.main.Main', {
    extend: 'Ext.container.Viewport',

    requires: [
        'Ext.button.Segmented',
        'Ext.list.Tree',
        'SimpleCMS.view.main.MainController',
        'SimpleCMS.view.main.MainModel',
        'SimpleCMS.view.main.MainContainerWrap',
        'SimpleCMS.view.authentication.*',
        'SimpleCMS.view.pages.*',
        'SimpleCMS.view.article.Main' //137页添加
    ],

    controller: 'main',
    viewModel: 'main',

    cls: 'sencha-dash-viewport',
    itemId: 'mainView',

    layout: {
        type: 'vbox',
        align: 'stretch'
    },

    listeners: {
        render: 'onMainViewRender'
    },

    items: [
        {
            xtype: 'toolbar',   //顶部工具栏，115页底部进行了修改
            cls: 'sencha-dash-dash-headerbar shadow',
            height: 64,
            itemId: 'headerBar',
            items: [
                {
                    xtype: 'component',  //logo图标
                    reference: 'senchaLogo',
                    cls: 'sencha-logo',
                    html: '<div class="main-logo"><img src="' + URI.getResource('logo') + '">' + I18N.AppTitle + '</div>',
                    width: 250
                },
                {
                    margin: '0 0 0 8',  //中间的空白组件
                    ui: 'header',
                    iconCls:'x-fa fa-navicon',
                    id: 'main-navigation-btn',
                    handler: 'onToggleNavigationSize'
                },
                '->', 
                {
                    iconCls: 'x-fa fa-key',   //139页添加的用户密码修改按钮
                    ui: 'header',
                    tooltip: I18N.PasswordResetTitle,
                    href: '#passwordreset',  //实际上就是Extjs路由，见137页上部
                    hrefTarget: '_self'
                },
                {
                    ui: 'header',                  //138页添加的退出按钮
                    iconCls: 'x-fa fa-power-off',
                    handler: 'onLogout',
                    tooltip: I18N.Logout
                },
                {
                    xtype: 'tbtext',        //用户名展示
                    bind: { text: '{UserName}' },
                    cls: 'top-user-name'
                }
                
            ]
        },
        {
            xtype: 'maincontainerwrap',
            id: 'main-view-detail-wrap',
            reference: 'mainContainerWrap',
            flex: 1,
            items: [
                {
                    xtype: 'treelist',
                    reference: 'navigationTreeList',
                    itemId: 'navigationTreeList',
                    ui: 'navigation',
                    store: 'NavigationTree',
                    width: 250,
                    expanderFirst: false,
                    expanderOnly: false,
                    listeners: {
                        selectionchange: 'onNavigationTreeSelectionChange'
                    }
                },
                {
                    xtype: 'container',
                    flex: 1,
                    reference: 'mainCardPanel',
                    cls: 'sencha-dash-right-main-container',
                    itemId: 'contentPanel',
                    layout: {
                        type: 'card',
                        anchor: '100%'
                    }
                }
            ]
        }
    ]
});
