/**
 * The main application class. An instance of this class is created by app.js when it
 * calls Ext.application(). This is the ideal place to handle application launch and
 * initialization details.
 */

//onAppUpdate方法在93页中间进行了修改，require配置项也是此处加入的
//'NavigationTree'导航树数据集的应用是115页底部加入的,指向store/NavigationTree.js文件.
Ext.define('SimpleCMS.Application', {
    extend: 'Ext.app.Application',
    
    name: 'SimpleCMS',

    requires: [
        'SimpleCMS.locale.Locale'
    ],

    stores: [
        // TODO: add global / shared stores here
        'NavigationTree'
    ],
    
    launch: function () {   //launch: function()是78页填充的内容
        // TODO - Launch the application
        Ext.util.Format.defaultValue = function (value, defaultValue) {
            return Ext.isEmpty(value) ? defaultValue : value;
        }
    },

    onAppUpdate: function () {
        Ext.Msg.confirm(I18N.ApplicationUpdate, I18N.ApplicationUpdateMsg,
            function (choice) {
                if (choice === 'yes') {
                    window.location.reload();
                }
            }
        );
    }
});


