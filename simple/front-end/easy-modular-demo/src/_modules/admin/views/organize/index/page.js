export default {
  title: '企业组织架构',
  name: 'admin_organize',
  path: '/admin/organize',
  icon: 'property',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_organize_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_organize_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_organize_delete_delete'
    },
    move: {
      text: '移动',
      type: 'text',
      icon: 'move',
      code: 'admin_tenant_move_post'
    }
  },
  component: () => import('./index')
}
