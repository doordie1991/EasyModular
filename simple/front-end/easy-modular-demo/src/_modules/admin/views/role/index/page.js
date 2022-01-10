export default {
  title: '角色管理',
  name: 'admin_role',
  path: '/admin/role',
  icon: 'property',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_role_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_role_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_role_delete_delete'
    },
    bindMenu: {
      text: '绑定菜单',
      type: 'text',
      icon: 'bind',
      code: 'admin_role_bindMenuPermission_post'
    }
  },
  component: () => import('./index')
}
