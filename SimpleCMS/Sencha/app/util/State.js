//85底部添加的文件,onStateChange部分是86页添加的部分.URI.get()方法调用的是自定义的类'SimpleCMS.util.Url'(5.8.10节)内的方法
//loaded变量和restore()方法体的添加位于89页底
Ext.define('SimpleCMS.util.State', {
    alternateClassName: 'STATE',
    singleton: true,

    requires: [
        'Ext.state.*',
        'Ext.util.LocalStorage',
        'SimpleCMS.util.Url'
    ],

    config: {
    },

    loaded: false,

    constructor: function (config) {
        var me = this,
            provider = Ext.util.LocalStorage.supported ? new Ext.state.LocalStorageProvider() : new Ext.state.CookieProvider();
        me.initConfig(config);
        me.callParent(arguments);
        Ext.state.Manager.setProvider(provider);
        provider.on('statechange', me.onStateChange, me);
    },

    onStateChange: function (provider, key, value) {
        var me = this;
        if (!me.loaded) return;
        if (value) {
            Ext.Ajax.request({
                url: URI.get('state', 'save'),
                params: { key: key, value: Ext.encode(value) }
            });
        }
    },

    restore: function () {
        var me = this;
        Ext.Ajax.request({
            async: false,
            url: URI.get('state', 'restore'),
            scope: me,
            success: function (response, opts) {
                var me = this,
                    obj = Ext.decode(response.responseText),
                    stateMgr = Ext.state.Manager,
                    ln,
                    i,
                    key,
                    value,
                    orgValue,
                    data;
                if (obj.success && obj.data) {
                    data = obj.data;
                    ln = data.length;
                    for (i = 0; i < ln; i++) {
                        key = data[i].Key;
                        value = Ext.decode(data[i].Value);
                        orgValue = stateMgr.get(key);
                        if (orgValue && JSON.stringify(orgValue) === JSON.stringify(value)) continue;
                        stateMgr.set(key, value);
                    }
                    me.loaded = true;
                }
            }
        });
    }

});
