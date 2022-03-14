//146页创建
Ext.define('SimpleCMS.view.user.Main', {
    extend: 'Ext.grid.Panel',
    //extend: 'SimpleCMS.ux.container.FixedHeightOfFirstItem',
    xtype: 'userView',

    requires: [
        'SimpleCMS.view.user.MainModel',
        'SimpleCMS.view.user.MainController',
    ],

    controller: 'user',
    viewModel: 'user',

    columns: [
        { xtype: 'rownumberer' },
        { text: I18N.UserModel.UserName, dataIndex: 'UserName', flex: 1, renderer: 'onHighLightRenderer' },
        { text: I18N.UserModel.Roles, dataIndex: 'Roles', width: 100 },
        { xtype: 'datecolumn', text: I18N.UserModel.Created, dataIndex: 'Created', format: I18N.DefaultDatetimeFormat, width: 150 },
        { xtype: 'datecolumn', text: I18N.UserModel.LastLogin, dataIndex: 'LastLogin', format: I18N.DefaultDatetimeFormat, width: 150 },
        { xtype: 'checkcolumn', text: I18N.UserModel.Lockout, dataIndex: 'Lockout', width: 100, listeners: { checkchange: 'onUserCheckChange' } },
        { xtype: 'checkcolumn', text: I18N.UserModel.IsApprove, dataIndex: 'IsApprove', width: 100, listeners: { checkchange: 'onUserCheckChange' } }
    ],

    selModel: {    //149页中间添加,使用复选模型
        selType: 'checkboxmodel',
        showHeaderCheckbox: false
    },

    cls: 'email-inbox-panel shadow', //149页添加的内容,遵从管理模板的风格
    headerBorders: false,
    rowLines: false

    /*
    items: [
        {
            xtype: 'grid',
           emptyText: I18N.EmptyText,  //140,网格没有数据时，网格内显示“没有任何数据”
            reference: 'UserGrid',
            columns: [
                { xtype: 'rownumberer' },
                { text: I18N.UserModel.UserName, dataIndex: 'UserName', flex: 1, renderer: 'onHighLightRenderer' },
                { text: I18N.UserModel.Roles, dataIndex: 'Roles', width: 100 },
                { xtype: 'datecolumn', text: I18N.UserModel.Created, dataIndex: 'Created', format: I18N.DefaultDatetimeFormat, width: 150 },
                { xtype: 'datecolumn', text: I18N.UserModel.LastLogin, dataIndex: 'LastLogin', format: I18N.DefaultDatetimeFormat, width: 150 },
                { xtype: 'checkcolumn', text: I18N.UserModel.Lockout, dataIndex: 'Lockout', width: 100, listeners: { checkchange: 'onUserCheckChange' } },
                { xtype: 'checkcolumn', text: I18N.UserModel.IsApprove, dataIndex: 'IsApprove', width: 100, listeners: { checkchange: 'onUserCheckChange' } }
            ],

            selModel: {    //149页中间添加,使用复选模型
                selType: 'checkboxmodel',
                showHeaderCheckbox: false
            },

            cls: 'email-inbox-panel shadow', //149页添加的内容,遵从管理模板的风格
            headerBorders: false,
            rowLines: false
        }
    ]
    */

})
