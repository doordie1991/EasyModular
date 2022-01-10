export default {
  title: '媒体类型',
  name: 'admin_media_type',
  path: '/admin/mediaType/index',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_mediaType_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_mediaType_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_mediaType_delete_delete'
    },
    import: {
      text: '导入',
      type: 'danger',
      icon: 'cloud-upload',
      code: 'admin_mediaType_import_post'
    },
  },
  component: () => import('./index')
}
