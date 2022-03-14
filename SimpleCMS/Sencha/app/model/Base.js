//143页创建 ,用户管理数据模型(注意前一章的用户登录没有用到，直接使用的是Identity提供的功能)
Ext.define('SimpleCMS.model.Base', {
    extend: 'Ext.data.Model',

    requires: [
        'Ext.data.identifier.Negative', //145A,使用的标识符生成器为负数生成器
        'SimpleCMS.locale.Locale'  //在基类中需要使用一次本地化类，基于基类的其它模型就不用多次引入了,146页上部添加
    ],

    fields: [    //144页添加的字段，避免后续模型重复定义ID字段
        { name: 'Id', type: 'int' }
    ],

    idProperty: 'Id', //指定id字段作为模型的标识符

    identifier: 'negative', //145,标识符生成使用负数生成器

    schema: {  //145,架构的配置
        namespace: 'SimpleCMS.model'
    }
});

