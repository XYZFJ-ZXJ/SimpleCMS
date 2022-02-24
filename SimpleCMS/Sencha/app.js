/*
 * This file is generated and updated by Sencha Cmd. You can edit this file as
 * needed for your application, but these edits will have to be merged by
 * Sencha Cmd when upgrading.
 */
//requires配置项使用的类，除了最后一个，Overrides是在78A添加的,必须在SimpleCMS.view.main.Main前,
// 'SimpleCMS.locale.Locale','SimpleCMS.locale.zh_CN','SimpleCMS.ux.*',是60页添加的内容. 'SimpleCMS.util.Config'是81页B添加的内容
// 'SimpleCMS.util.Url'是81页底部加入的内容. 'SimpleCMS.util.State'是90页添加的.'SimpleCMS.util.Toast'是91页添加的.
//'SimpleCMS.util.Failed'是100页添加的
Ext.application({
    name: 'SimpleCMS',

    extend: 'SimpleCMS.Application',

    requires: [
        'Overrides.*',
        'SimpleCMS.locale.Locale',
        'SimpleCMS.locale.zh_CN',
        'SimpleCMS.util.Config',
        'SimpleCMS.util.Url',
        'SimpleCMS.util.State',
        'SimpleCMS.util.Failed',
        'SimpleCMS.util.Toast',
        'SimpleCMS.ux.*',
        'SimpleCMS.view.main.Main'
    ],

    // The name of the initial view to create. With the classic toolkit this class
    // will gain a "viewport" plugin if it does not extend Ext.Viewport. With the
    // modern toolkit, the main view will be added to the Viewport.
    //
    mainView: 'SimpleCMS.view.main.Main'
	
    //-------------------------------------------------------------------------
    // Most customizations should be made to SimpleCMS.Application. If you need to
    // customize this file, doing so below this section reduces the likelihood
    // of merge conflicts when upgrading to new versions of Sencha Cmd.
    //-------------------------------------------------------------------------
});
