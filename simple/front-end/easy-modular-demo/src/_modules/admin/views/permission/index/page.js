export default {
  title: '权限信息',
  name: 'admin_permission',
  path: '/admin/permission',
  icon: 'property',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_permission_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_permission_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_permission_delete_delete'
    },
    sync: {
      text: '同步',
      type: 'danger',
      icon: 'sync',
      code: 'admin_permission_sync_get'
    }
  },
  component: () => import('./index')
}
