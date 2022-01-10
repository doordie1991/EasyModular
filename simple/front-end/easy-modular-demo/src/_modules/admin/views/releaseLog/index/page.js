export default {
  title: '发布记录',
  name: 'admin_release_log',
  path: '/admin/release/log',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_releaseLog_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_releaseLog_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_releaseLog_delete_delete'
    }
  },
  component: () => import('./index')
}
