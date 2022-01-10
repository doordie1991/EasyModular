export default {
  title: '字典列表',
  name: 'admin_dictionary',
  path: '/admin/dictionary',
  icon: 'property',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_dictionary_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_dictionary_edit_get'
    },
    enable: {
      text: '启用',
      type: 'text',
      icon: 'run',
      code: 'admin_dictionary_enable_post'
    },
    disable: {
      text: '禁用',
      type: 'text',
      icon: 'pause',
      code: 'admin_dictionary_disable_post'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_dictionary_delete_delete'
    },
    item: {
      text: '管理数据项',
      icon: 'edit',
      type: 'text',
      // code: `admin_dictionary_item`
    }
  },
  component: () => import('./index')
}
