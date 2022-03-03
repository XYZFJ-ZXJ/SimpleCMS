//121页底部对本类进行了修改,删除了几个方法，添加了onReturnClick方法
Ext.define('SimpleCMS.view.authentication.AuthenticationController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.authentication',

    //TODO: implement central Facebook OATH handling here

    onLoginButton: function() {
        this.redirectTo('dashboard', true);
    },

    onResetClick:  function() {
        this.redirectTo('dashboard', true);
    },

    onReturnClick: function () {
        window.history.back();
    }
});