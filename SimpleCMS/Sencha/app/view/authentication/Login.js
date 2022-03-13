//120页底部对该文件进行了修改，125页C进行了修改，image项是126页添加的
Ext.define('SimpleCMS.view.authentication.Login', {
    extend: 'SimpleCMS.view.authentication.LockingWindow',
    xtype: 'login',

    requires: [
        'SimpleCMS.view.authentication.Dialog',
        'Ext.container.Container',
        'Ext.form.field.Text',
        'Ext.form.field.Checkbox',
        'Ext.button.Button'
    ],

    title: I18N.LoginTitle,
    defaultFocus: 'authdialog', // Focus the Auth Form to force field focus as well

    items: [
        {
            xtype: 'authdialog',
            defaultButton : 'loginButton',
            autoComplete: true,
            bodyPadding: '20 20',
            cls: 'auth-dialog-login',
            header: false,
            width: 415,
            layout: {
                type: 'vbox',
                align: 'stretch'
            },

            defaults : {
                margin : '5 0'
            },

            items: [
                {
                    xtype: 'label',
                    text: I18N.LoginLabel
                },
                {
                    xtype: 'textfield',
                    cls: 'auth-textbox',
                    name: 'UserName',
                    height: 55,
                    hideLabel: true,
                    allowBlank : false,
                    emptyText: I18N.UserId,
                    triggers: {
                        glyphed: {
                            cls: 'trigger-glyph-noop auth-email-trigger'
                        }
                    }
                },
                {
                    xtype: 'textfield',
                    cls: 'auth-textbox',
                    height: 55,
                    hideLabel: true,
                    emptyText: I18N.Password,
                    inputType: 'password',
                    name: 'Password',
                    allowBlank : false,
                    triggers: {
                        glyphed: {
                            cls: 'trigger-glyph-noop auth-password-trigger'
                        }
                    }
                },
                {
                    xtype: 'textfield',
                    cls: 'auth-textbox',
                    name: 'VerifyCode',
                    height: 55,
                    hideLabel: true,
                    allowBlank: false,
                    maxLength: 6,
                    minLength: 6,

                    emptyText: I18N.VerifyCode,
                    triggers: {
                        glyphed: {
                            cls: 'trigger-glyph-noop auth-verifycode-trigger'
                        }
                    }
                },
                {
                    xtype: 'image', //126页底部添加，使用图片组件来显示验证码
                    height: 55,
                    src: '',
                    title: I18N.VerifyCodeAlt,      //为127页新添加的内容,实现验证码图片的单击刷新功能 
                    alt: I18N.VerifyCodeAlt,
                    style: 'cursor:pointer',
                    listeners: {
                        click: {
                            fn: 'onRefrestVcode',//调用的AuthenticationController.js方法
                            element: 'el'
                        }
                    }
                },
                {
                    xtype: 'container',
                    layout: 'hbox',
                    items: [
                        {
                            xtype: 'checkboxfield',
                            flex : 1,
                            cls: 'form-panel-font-color rememberMeCheckbox',
                            height: 30,
                            name: 'RemberMe',
                            boxLabel: I18N.RememberMe
                        }
                    ]
                },
                {
                    xtype: 'button',
                    reference: 'loginButton',
                    scale: 'large',
                    ui: 'soft-green',
                    iconAlign: 'right',
                    iconCls: 'x-fa fa-angle-right',
                    text: I18N.LoginTitle,
                    formBind: true,
                    listeners: {
                        click: 'onLoginButton'  //定义在authenticationcontroller.js内
                    }
                }
            ]
        }
    ],

    initComponent: function() {
        this.addCls('user-login-register-container');
        this.callParent(arguments);
    },

    listeners: {     //show事件是127页上部添加的，实现每次进入登录视图都能刷新验证码
        show: 'onLoginViewShow'
    }
});
