//116ҳ�ײ�ɾ����children�������ڵ��ӽڵ㣬��118ҳ�ײ��������һ���հ�ҳ�ڵ�,500��ͼ������120ҳ��ӵģ���¼��ͼ��120ҳ�ײ���ӵ�,�޸�������ͼ������122ҳ��ӵ�
Ext.define('SimpleCMS.store.NavigationTree', {
    extend: 'Ext.data.TreeStore',

    storeId: 'NavigationTree',

    fields: [{
        name: 'text'
    }],

    root: {
        expanded: true,
        children: [
            {
                text: '�հ�ҳ',
                viewType: 'pageblank',
                leaf: true,
                visible: false
            },
            {
                text: '500��ͼ',
                viewType: 'page500',
                leaf: true,
                visible: false
            },
            {
                text: '��¼��ͼ',
                viewType: 'login',
                leaf: true,
                visible: false
            },
            {
                text: '�޸�������ͼ',
                viewType: 'passwordreset',
                leaf: true,
                visible: false
            }
        ]
    }
});
