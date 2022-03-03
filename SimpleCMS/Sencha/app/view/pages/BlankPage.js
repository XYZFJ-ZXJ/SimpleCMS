//118页A处作出了修改，并且requires配置项不需要在其中配置local本地化类，因为本地化类是静态类，已经在Application.js类中配置过了
Ext.define('SimpleCMS.view.pages.BlankPage', {
    extend: 'Ext.container.Container',
    xtype: 'pageblank',

    requires: [
        'Ext.container.Container'
    ],

    anchor : '100% -1',

    layout:{
        type:'vbox',
        pack:'center',
        align:'center'
    },

    items: [
        {
            xtype: 'box',
            cls: 'blank-page-container',
            html: '<div class=\'fa-outer-class\'><span class=\'x-fa fa-clock-o\'></span></div><h1>' + I18N.ComingSoon + '</h1><span class=\'blank-page-text\'>' + I18N.StayTunedForUpdates + '</span>'
        }
    ]
});
