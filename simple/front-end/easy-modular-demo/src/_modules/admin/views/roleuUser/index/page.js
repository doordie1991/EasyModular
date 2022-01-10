export default {
  title: '用户管理',
  name: 'admin_role_user',
  path: '/admin/role/user',
  icon: 'property',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_roleUser_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_roleUser_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_roleUser_delete_delete'
    }
  },
  component: () => import('./index')
}
