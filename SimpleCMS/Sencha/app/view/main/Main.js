//'SimpleCMS.view.main.MainController','SimpleCMS.view.main.MainModel','SimpleCMS.view.main.MainContainerWrap'��115ҳ�ײ����������.
//116ҳ�޸���logo�����html�����������.��ʾ�û������text�������޸�Ϊbind�����116����'SimpleCMS.locale.Locale','SimpleCMS.util.Url'�������Ҫ���뱾��requires�������У�
//��Ϊ���ػ����Ǿ�̬�࣬�Ѿ���Application.js�������ù���.'SimpleCMS.view.authentication.*','SimpleCMS.view.pages.*'��118ҳ�ϲ���ӵ�.
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
        'SimpleCMS.view.article.Main' //137ҳ���
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
            xtype: 'toolbar',   //������������115ҳ�ײ��������޸�
            cls: 'sencha-dash-dash-headerbar shadow',
            height: 64,
            itemId: 'headerBar',
            items: [
                {
                    xtype: 'component',  //logoͼ��
                    reference: 'senchaLogo',
                    cls: 'sencha-logo',
                    html: '<div class="main-logo"><img src="' + URI.getResource('logo') + '">' + I18N.AppTitle + '</div>',
                    width: 250
                },
                {
                    margin: '0 0 0 8',  //�м�Ŀհ����
                    ui: 'header',
                    iconCls:'x-fa fa-navicon',
                    id: 'main-navigation-btn',
                    handler: 'onToggleNavigationSize'
                },
                '->', 
                {
                    iconCls: 'x-fa fa-key',   //139ҳ��ӵ��û������޸İ�ť
                    ui: 'header',
                    tooltip: I18N.PasswordResetTitle,
                    href: '#passwordreset',  //ʵ���Ͼ���Extjs·�ɣ���137ҳ�ϲ�
                    hrefTarget: '_self'
                },
                {
                    ui: 'header',                  //138ҳ��ӵ��˳���ť
                    iconCls: 'x-fa fa-power-off',
                    handler: 'onLogout',
                    tooltip: I18N.Logout
                },
                {
                    xtype: 'tbtext',        //�û���չʾ
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
