//97页底部添加的文件,requires:配置项和listeners配置项是101页加入的配置项
Ext.define('SimpleCMS.ux.data.proxy.Format', {
    extend: 'Ext.data.proxy.Ajax',
    alias: 'proxy.format',

    requires: [
        'SimpleCMS.util.Failed'
    ],

    reader: {
        type: 'json',
        rootProperty: "data",
        messageProperty: "msg"
    },

    writer: {
        type: "json",
        encode: true,
        rootProperty: "data",
        allowSingle: false
    },

    listeners: {
        exception: FAILED.proxy
    }

})

