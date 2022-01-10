export default {
  title: '字典分组',
  name: 'admin_dictionary_group',
  path: '/admin/dictionary/group',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_dictionaryGroup_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_dictionaryGroup_edit_get'
    },
    enable: {
      text: '启用',
      type: 'text',
      icon: 'run',
      code: 'admin_dictionaryGroup_enable_post'
    },
    disable: {
      text: '禁用',
      type: 'text',
      icon: 'pause',
      code: 'admin_dictionaryGroup_disable_post'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_dictionaryGroup_delete_delete'
    }
  },
  component: () => import('./index')
}

