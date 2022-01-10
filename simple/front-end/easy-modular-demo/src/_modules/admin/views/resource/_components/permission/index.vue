<template>
  <em-dialog class="resource-permission" v-bind="dialog" :visible.sync="visible_" @opened="onOpened">
    <el-tabs type="border-card">
      <el-tab-pane label="权限设置">
        <em-panel header title="过滤条件" icon="appstore" style="margin-top: 8px">
          <el-table :data="form.model.filters" style="width: 100%" border :size="fontSize">
            <el-table-column prop="fieldName" label="字段名称" align="center">
              <template slot-scope="scope">
                <filed-select :resourceId="id" v-model="scope.row.fieldName"></filed-select>
              </template>
            </el-table-column>
            <el-table-column prop="conditionalType" label="操作符" align="center">
              <template slot-scope="scope">
                <conditional-type-select v-model="scope.row.conditionalType"></conditional-type-select>
              </template>
            </el-table-column>
            <el-table-column prop="fieldValueType" label="字段值类别" align="center">
              <template slot-scope="scope">
                <el-radio v-model="scope.row.fieldValueType" :label="0">常量</el-radio>
                <el-radio v-model="scope.row.fieldValueType" :label="1">变量</el-radio>
              </template>
            </el-table-column>
            <el-table-column prop="fieldValue" label="字段值" align="center">
              <template slot-scope="scope">
                <el-input v-if="scope.row.fieldValueType === 0" v-model="scope.row.fieldValue" clearable :size="fontSize" />
                <em-dict-select v-else-if="scope.row.fieldValueType === 1" group="admin" code="loginInfo" v-model="scope.row.fieldValue" :size="fontSize"></em-dict-select>
              </template>
            </el-table-column>
            <el-table-column label="操作" align="center" width="100">
              <template slot-scope="scope">
                <em-button type="text" icon="delete" @click="deleteFilter(scope.$index)">移除</em-button>
              </template>
            </el-table-column>
          </el-table>

          <div class="add-btn">
            <em-button type="primary" icon="add" size="mini" @click="addFilter">新增</em-button>
          </div>
        </em-panel>
      </el-tab-pane>
      <el-tab-pane label="白名单设置">
        <div class="add-btn">
          <em-button type="primary" icon="user" size="mini" @click="userShow = true">选择用户</em-button>
          <em-button type="danger" icon="redis" size="mini" @click="roleShow = true">选择角色</em-button>
        </div>
        <div class="whitelist">
          <whitelist-item v-for="(item, index) in form.model.whitelists" :data="item" :key="index" @delete="onDeleteWhitelist"></whitelist-item>
        </div>
      </el-tab-pane>
    </el-tabs>
    <template v-slot:footer>
      <em-button type="success" size="small" @click="confirm">确定</em-button>
      <em-button type="info" size="small" @click="visible_ = false">关闭</em-button>
    </template>

    <!--用户选择-->
    <em-user-select-dialog :visible.sync="userShow" @change="addUser"></em-user-select-dialog>
    <!--角色选择-->
    <role-select :visible.sync="roleShow" @change="addRole"></role-select>
  </em-dialog>
</template>

<script>
import { mixins } from 'easy-modular-ui'
import FiledSelect from '../filed-select'
import WhitelistItem from '../whitelist-item'
import RoleSelect from '../../../role/_components/list-select'
import ConditionalTypeSelect from '../conditional-type-select'
export default {
  mixins: [mixins.visible],
  components: { FiledSelect, WhitelistItem, RoleSelect, ConditionalTypeSelect },
  props: {
    //资源Id
    id: {
      type: String,
      require: true
    },
    //资源名称
    title: {
      type: String,
      require: true
    }
  },
  data() {
    return {
      dialog: {
        title: '资源权限设置',
        icon: 'block',
        width: '900px',
        height: '700px',
        noScrollbar: true,
        footer: true,
        padding: 6
      },
      form: {
        model: {
          //过滤
          filters: [],
          //白名单
          whitelists: []
        }
      },
      userShow: false,
      roleShow: false
    }
  },
  methods: {
    /**
     * @description: 获取数据
     * @param {*}
     */
    async getData(id) {
      const data = await $api.admin.resource.edit(id)
      Object.assign(this.form.model, data)
    },

    /**
     * @description: 移除过滤
     * @param {*}
     */
    deleteFilter(index) {
      this.form.model.filters.splice(index, 1)
    },

    /**
     * @description: 添加过滤
     * @param {*}
     */
    addFilter() {
      this.form.model.filters.push({ fieldName: '', conditionalType: null, fieldValueType: null, fieldValue: '' })
    },

    /**
     * @description: 添加用户
     * @param {*}
     */
    addUser(rows) {
      rows.forEach((v) => {
        if (!this.form.model.whitelists.find((m) => m.value == v.id)) {
          this.form.model.whitelists.push({ value: v.id, valueType: 0 })
        }
      })
    },

    /**
     * @description: 添加角色
     * @param {*}
     */
    addRole(rows) {
      rows.forEach((v) => {
        if (!this.form.model.whitelists.find((m) => m.value == v.id)) {
          this.form.model.whitelists.push({ value: v.id, valueType: 1 })
        }
      })
    },

    /**
     * @description: 移除白名单
     * @param {*}
     */
    async onDeleteWhitelist(data) {
      await this._confirm('确认移除吗?')
      const index = this.form.model.whitelists.findIndex((m) => m.value == data.value)
      if (index >= 0) this.form.model.whitelists.splice(index, 1)
    },

    /**
     * @description: 确认
     * @param {*}
     */
    async confirm() {
      this.form.model.filters.forEach((m) => {
        m.resourceId = this.id
      })

      this.form.model.whitelists.forEach((m) => {
        m.resourceId = this.id
      })
      await $api.admin.resource.permissions(this.form.model)
      await this._success('保存成功')
      this.hide()
    },

    /**
     * @description:窗口打开后回调
     * @param {*}
     */
    onOpened() {}
  },
  watch: {
    id: {
      handler(n, o) {
        if (n) this.getData(n)
      },
      immediate: true
    },
    title: {
      handler(n, o) {
        if (n != o) this.dialog.title = n
      },
      immediate: true
    }
  }
}
</script>

<style lang="scss">
.resource-permission {
  .el-tabs,
  .el-tabs__content,
  .el-tab-pane {
    height: 100%;
  }
  .el-tabs__nav-scroll {
    background: #f8f8f8;
  }

  .add-btn {
    display: flex;
    width: 100%;
    height: 50px;
    justify-content: center;
    align-items: center;
    border: 1px solid #d8d9da;
    border-radius: 6px;
    margin-top: 12px;
    box-shadow: 0px 0px 8px #bec2c5;
  }

  .whitelist {
    display: flex;
    flex-wrap: wrap;
    margin-top: 20px;
  }
}
</style>
