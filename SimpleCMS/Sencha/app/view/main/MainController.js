//116页顶部删除了一些迁移过来的东西,parentNode那行代码是116页底部添加的.arentNode.expand()是117页上部
Ext.define('SimpleCMS.view.main.MainController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.main',

    listen : {
        controller : {
            '#' : {
                unmatchedroute : 'onRouteChange'
            }
        }
    },

    routes: {
        ':node': 'onRouteChange'
    },

    isLogin: false, //136页添加，用来阻止先执行路由事件，后执行登录处理这一默认规则导致登录本能显示main视图而一直是login视图的问题 
    lastView: null,

    setCurrentView: function(hashTag) {
        hashTag = (hashTag || '').toLowerCase();
        //136页加入的内容，未真正登录完成直接不进入main,那么久不会被mian再次路由到login视图了
        if (!this.isLogin && hashTag !== 'login') return; 
        var me = this,
            refs = me.getReferences(),
            mainCard = refs.mainCardPanel,
            mainLayout = mainCard.getLayout(),
            navigationList = refs.navigationTreeList,
            store = navigationList.getStore(),
            node = store.findNode('routeId', hashTag) ||
                   store.findNode('viewType', hashTag),
            view = (node && node.get('viewType')) || 'page404',
            lastView = me.lastView,
            existingItem = mainCard.child('component[routeId=' + hashTag + ']'),
            parentNode = node ? node.parentNode : null,
            newView;

        // Kill any previously routed window
        if (lastView && lastView.isWindow) {
            lastView.destroy();
        }

        lastView = mainLayout.getActiveItem();

        if (!existingItem) {
            newView = Ext.create({
                xtype: view,
                routeId: hashTag,  // for existingItem search later
                hideMode: 'offsets'
            });
        }

        if (!newView || !newView.isWindow) {
            // !newView means we have an existing view, but if the newView isWindow
            // we don't add it to the card layout.
            if (existingItem) {
                // We don't have a newView, so activate the existing view.
                if (existingItem !== lastView) {
                    mainLayout.setActiveItem(existingItem);
                }
                newView = existingItem;
            }
            else {
                // newView is set (did not exist already), so add it and make it the
                // activeItem.
                Ext.suspendLayouts();
                mainLayout.setActiveItem(mainCard.add(newView));
                Ext.resumeLayouts(true);
            }
        }

        if (parentNode && !parentNode.isRoot() && !parentNode.isExpanded()) parentNode.expand();
        navigationList.setSelection(node);

        if (newView.isFocusable(true)) {
            newView.focus();
        }

        me.lastView = newView;
    },

    onNavigationTreeSelectionChange: function (tree, node) {
        var to = node && (node.get('routeId') || node.get('viewType'));

        if (to) {
            this.redirectTo(to);
        }
    },

    onToggleNavigationSize: function () {
        var me = this,
            refs = me.getReferences(),
            navigationList = refs.navigationTreeList,
            wrapContainer = refs.mainContainerWrap,
            collapsing = !navigationList.getMicro(),
            new_width = collapsing ? 64 : 250;

        if (Ext.isIE9m || !Ext.os.is.Desktop) {
            Ext.suspendLayouts();

            refs.senchaLogo.setWidth(new_width);

            navigationList.setWidth(new_width);
            navigationList.setMicro(collapsing);

            Ext.resumeLayouts(); // do not flush the layout here...

            // No animation for IE9 or lower...
            wrapContainer.layout.animatePolicy = wrapContainer.layout.animate = null;
            wrapContainer.updateLayout();  // ... since this will flush them
        }
        else {
            if (!collapsing) {
                // If we are leaving micro mode (expanding), we do that first so that the
                // text of the items in the navlist will be revealed by the animation.
                navigationList.setMicro(false);
            }

            // Start this layout first since it does not require a layout
            refs.senchaLogo.animate({dynamic: true, to: {width: new_width}});

            // Directly adjust the width config and then run the main wrap container layout
            // as the root layout (it and its chidren). This will cause the adjusted size to
            // be flushed to the element and animate to that new size.
            navigationList.width = new_width;
            wrapContainer.updateLayout({isRoot: true});
            navigationList.el.addCls('nav-tree-animating');

            // We need to switch to micro mode on the navlist *after* the animation (this
            // allows the "sweep" to leave the item text in place until it is no longer
            // visible.
            if (collapsing) {
                navigationList.on({
                    afterlayoutanimation: function () {
                        navigationList.setMicro(true);
                        navigationList.el.removeCls('nav-tree-animating');
                    },
                    single: true
                });
            }
        }
    },
    //132页进行修改，在主页面显示渲染完毕后的时刻跳出登录凳面，要和后端AccountController的UserInfo方法共同实现跳转登录页的功能.
    onMainViewRender:function() {
        //if (!window.location.hash) {
        //    this.redirectTo("dashboard");
        //}
        var me = this;  //133页顶部添加的内容，135又再增加的内容
        Ext.Msg.wait(I18N.GetUserInfo);
        Ext.Ajax.request({
            url: URI.get('account', 'userinfo'),
            success: function (response, opts) {
                var me = this,
                    refs = me.getReferences(),
                    navigationList = refs.navigationTreeList,
                    store = navigationList.getStore(),
                    root = store.getRoot(),
                    viewModel = me.getViewModel(),
                    obj = Ext.decode(response.responseText, true),
                    hash, node, parentNode, roles;
                Ext.Msg.hide();
                if (Ext.isEmpty(obj) || !obj.success) {
                    me.setCurrentView("login");
                    return;
                }
                viewModel.set('UserName', obj.data.UserInfo.UserName);
                CFG.setUserInfo(obj.data.UserInfo);
                root.appendChild(obj.data.Menu);
                Ext.Msg.wait(I18N.StateRestoreWait);  //85页添加的内容，用于视图状态恢复
                STATE.restore();
                Ext.Msg.hide();
                me.isLogin = true;    //136页添加的内容
                hash = window.location.hash.substr(1);//在主视图中显示默认视图<显示位置为主视图右下角的最主要的视区>，见137页
                me.setCurrentView(Ext.isEmpty(hash) || hash === 'login' ? "articleView" : hash);
            },
            failure: FAILED.ajax,   //全局Ajax错误处理类，见100页
            scope: me
        });
    },

    onRouteChange:function(id){
        this.setCurrentView(id);
    },
     //138页添加，用于登出操作（登出操作不涉及Identity验证，所以本方法不用写在authentication/AuhhenticationController.js内）
    onLogout: function () { 
        Ext.Ajax.request({
            url: URI.get('account', 'logout'),
            scope: this,
            success: function (response, opts) {
                window.location.reload();
            }
        });
    }

});
