export default {
  title: '菜单管理',
  name: 'admin_menu',
  path: '/admin/menu',
  icon: 'property',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_menu_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_menu_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_menu_delete_delete'
    },
    bindBtn: {
      text: '权限按钮',
      type: 'text',
      icon: 'block',
      code: 'admin_menu_bindbtn_post'
    },
    move: {
      text: '移动',
      type: 'text',
      icon: 'move',
      code: 'admin_menu_move_post'
    }
  },
  component: () => import('./index')
}
