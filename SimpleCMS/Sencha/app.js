/*
 * This file is generated and updated by Sencha Cmd. You can edit this file as
 * needed for your application, but these edits will have to be merged by
 * Sencha Cmd when upgrading.
 */
//requires������ʹ�õ��࣬�������һ����Overrides����78A��ӵ�,������SimpleCMS.view.main.Mainǰ,
// 'SimpleCMS.locale.Locale','SimpleCMS.locale.zh_CN','SimpleCMS.ux.*',��60ҳ��ӵ�����. 'SimpleCMS.util.Config'��81ҳB��ӵ�����
// 'SimpleCMS.util.Url'��81ҳ�ײ����������. 'SimpleCMS.util.State'��90ҳ��ӵ�.'SimpleCMS.util.Toast'��91ҳ��ӵ�.
//'SimpleCMS.util.Failed'��100ҳ��ӵ�
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
