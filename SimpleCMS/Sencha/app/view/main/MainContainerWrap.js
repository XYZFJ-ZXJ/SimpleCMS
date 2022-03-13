Ext.define('SimpleCMS.view.main.MainContainerWrap', { //为Main.js中的主容器，即mian下vbox布局的下面部分，即整个视区的主要部分(除了上边的工具栏)
    extend: 'Ext.container.Container',
    xtype: 'maincontainerwrap',

    requires : [
        'Ext.layout.container.HBox'
    ],

    scrollable: 'y',

    layout: {
        type: 'hbox',
        align: 'stretchmax',

        // Tell the layout to animate the x/width of the child items.
        animate: true,
        animatePolicy: {
            x: true,
            width: true
        }
    },

    beforeLayout : function() {
        // We setup some minHeights dynamically to ensure we stretch to fill the height
        // of the viewport minus the top toolbar

        var me = this,
            height = Ext.Element.getViewportHeight() - 64,  // offset by topmost toolbar height
            // We use itemId/getComponent instead of "reference" because the initial
            // layout occurs too early for the reference to be resolved
            navTree = me.getComponent('navigationTreeList');

        me.minHeight = height;

        navTree.setStyle({
            'min-height': height + 'px'
        });

        me.callParent(arguments);
    }
});
