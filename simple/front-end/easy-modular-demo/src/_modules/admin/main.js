import './api'
import store from './store'
import routes from './routes'
import PasswordUpdate from './views/passwordUpdate'
import ModuleSelect from './views/module/_components/select'
import DictSelect from './views/dictionaryItem/_components/select'
import AttachmentImg from './components/attachment-img'
import AttachmentUploadImg from './components/attachment-upload-img'
import AttachmentUploadImgList from './components/attachment-upload-img-list'
import AttachmentUpload from './components/attachment-upload'
import UserSelect from './views/user/_components/select'
import UserSelectDialog from './views/user/_components/dialog'
import UserSelectMulti from './components/user-select-multi'
import UserSelectSingle from './components/user-select-single'
import RoleUserSelect from './views/roleuUser/_components/select'
import FileTemplate from './components/file-template'
import ImportData from './components/import-data'

export default {
  module: {
    name: 'em-module-admin',
    code: 'admin',
    version: '1.0.0',
    description: '系统管理'
  },
  routes,
  store,
  components: [
    { name: 'em-password-update', component: PasswordUpdate },
    { name: 'em-module-select', component: ModuleSelect },
    { name: 'em-dict-select', component: DictSelect },
    { name: 'em-attachment-upload', component: AttachmentUpload },
    { name: 'em-attachment-img', component: AttachmentImg },
    { name: 'em-attachment-upload-img', component: AttachmentUploadImg },
    { name: 'em-attachment-upload-img-list', component: AttachmentUploadImgList },
    { name: 'em-user-select', component: UserSelect },
    { name: 'em-user-select-dialog', component: UserSelectDialog },
    { name: 'em-user-select-multi', component: UserSelectMulti },
    { name: 'em-user-select-single', component: UserSelectSingle },
    { name: 'em-role-user-select', component: RoleUserSelect },
    { name: 'em-import-data', component: ImportData },
    { name: 'em-file-template', component: FileTemplate }
  ]
}
