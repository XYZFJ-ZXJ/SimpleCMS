//91页创建的文件,重写Extjs的信息提示组件
Ext.define('SimpleCMS.util.Toast', {
    alternateClassName: 'TOAST',
    singleton: true,

    toast: function (message, el, align, fn, scope, args) {
        var toast = Ext.create('Ext.window.Toast',
            {
                html: message,
                closable: false,
                anchor: el || Ext.getBody(),
                align: align || 'bl',
                minWidth: 400
            });
        if (Ext.isFunction(fn)) {
            toast.on('close', fn, scope, { single: true, args: args });
        }
        toast.show();
    }
});
