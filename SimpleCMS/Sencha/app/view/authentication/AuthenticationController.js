//121页底部对本类进行了修改,删除了几个方法，添加了onReturnClick方法
Ext.define('SimpleCMS.view.authentication.AuthenticationController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.authentication',

    //TODO: implement central Facebook OATH handling here

    onLoginButton: function () {
        //this.redirectTo('dashboard', true);
        //迁移过来的文件就有该函数名,指定在了login.js内,129页修改和填充方法体,用于提交登录表单
        var me = this,
            f = me.getView().getForm();
        if (f.isValid()) {
            f.submit({
                url: URI.get('account', 'login'),
                waitMsg: I18N.LoginSubmitWaitMsg,
                waitTitle: I18N.LoginSubmitWaitTitle,
                success: function (form, action) {
                    window.location.reload();
                },
                failure: FAILED.form,
                scope: me
            });
        }
    },

    //139页底进行了修改，是PasswordRest.js用户密码修改视图的表单提交方法.
    onResetClick:  function() {
        //this.redirectTo('dashboard', true);  原来代码
        var me = this,
            view = me.getView(),
            f = view.getForm();
        if (f.isValid()) {
            f.submit({
                url: URI.get('account', 'passwordreset'),
                waitMsg: I18N.SaveWaitMsg,
                waitTitle: I18N.PasswordResetTitle,
                success: function (form, action) {
                    TOAST.toast(I18N.PasswordResetSuccess, view.el, null, function () {
                        window.location.reload();

                    });
                },
                failure: FAILED.form,
                scope: me
            });
        }
    },

    onReturnClick: function () {
        window.history.back();
    },

    verifyCodeUrl: URI.get('VerifyCode', ''),  //以下部分为127页添加

    onRefrestVcode: function () {
        var me = this,
            view = me.getView(),
            img = view.down('image');
        img.setSrc(me.verifyCodeUrl + '?_dc=' + (new Date().getTime()));
    },

    onLoginViewShow: function () {
        this.onRefrestVcode();
    }

});