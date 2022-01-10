export default {
  title: '用户管理',
  name: 'admin_user',
  path: '/admin/user',
  icon: 'property',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_user_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_user_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_user_delete_delete'
    },
    enable: {
      text: '启用',
      type: 'text',
      icon: 'run',
      code: 'admin_user_enable_post'
    },
    disable: {
      text: '禁用',
      type: 'text',
      icon: 'stop',
      code: 'admin_user_disable_post'
    },
    move: {
      text: '更新组织',
      type: 'text',
      icon: 'move',
      code: 'admin_user_move_post'
    }
  },
  component: () => import('./index')
}
