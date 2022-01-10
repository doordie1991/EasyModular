<template>
  <em-dialog v-bind="dialog" :visible.sync="visible_" footer @opened="onOpened">
    <el-table :data="buttons" style="width: 100%" border :size="fontSize">
      <el-table-column prop="buttonName" label="按钮名称" width="180" align="center">
        <template slot-scope="scope">
          <el-input :size="fontSize" v-model="scope.row.buttonName" placeholder="请输入按钮名称" />
        </template>
      </el-table-column>
      <el-table-column prop="permissionCode" label="权限编码" width="250" align="center">
        <template slot-scope="scope">
          <el-input :size="fontSize" v-model="scope.row.permissionCode" placeholder="请输入权限编码" />
        </template>
      </el-table-column>
      <el-table-column prop="icon" label="图标" align="center">
        <template slot-scope="scope">
          <el-input :size="fontSize" v-model="scope.row.icon" placeholder="请输入图标编码" />
        </template>
      </el-table-column>
      <el-table-column prop="iconColor" label="图标颜色" align="center">
        <template slot-scope="scope">
          <el-input :size="fontSize" v-model="scope.row.iconColor" placeholder="请输入图标颜色" />
        </template>
      </el-table-column>
      <el-table-column prop="sort" label="排序" align="center" width="100">
        <template slot-scope="scope">
          <el-input-number :size="fontSize" v-model="scope.row.sort" placeholder="请输入排序" :controls="false" style="width: 60px" />
        </template>
      </el-table-column>
      <el-table-column prop="modifiedTime" label="修改时间" width="100" align="center" :show-overflow-tooltip="true"> </el-table-column>
      <el-table-column label="操作" align="center">
        <template slot-scope="scope">
          <em-button type="text" icon="delete" @click="onRemove(scope.$index)">移除</em-button>
        </template>
      </el-table-column>
    </el-table>
    <div class="add-btn">
      <em-button type="danger" icon="search" size="mini" @click="permissionSelectShow = true">从系统权限选择</em-button>
      <em-button type="primary" icon="add" size="mini" @click="onAdd">新增</em-button>
    </div>
    <template v-slot:footer>
      <em-button type="success" text="确认" @click="onConfirm" />
      <em-button type="info" text="取消" @click="hide" />
    </template>

    <permission-select :visible.sync="permissionSelectShow" @change="onSelect"></permission-select>
  </em-dialog>
</template>

<script>
import { mixins } from 'easy-modular-ui'
import permissionSelect from '../../../permission/_components/list-select'
export default {
  mixins: [mixins.visible],
  components: { permissionSelect },
  data() {
    return {
      dialog: {
        title: '按钮权限设置',
        icon: 'app',
        width: '1000px',
        height: '600px'
      },
      buttons: [],
      permissionSelectShow: false
    }
  },
  props: ['menuId', 'menuCode'],
  methods: {
    /**
     * @description: 获取按钮
     * @param {*}
     */
    async getData() {
      this.buttons = await $api.admin.menuButton.queryByMenuId(this.menuId)
    },

    /**
     * @description: 确认
     * @param {*}
     */
    async onConfirm() {
      const data = { menuId: this.menuId, buttons: this.buttons }
      const result = await $api.admin.menu.bindBtn(data)
      await this._success('保存成功')
      this.hide()
    },

    /**
     * @description: 添加按钮
     * @param {*}
     */
    onAdd() {
      this.buttons.push({ sort: this.buttons.length + 1 })
    },

    /**
     * @description:选择系统权限成功回调事件
     * @param {*}
     */
    onSelect(rows) {
      rows.forEach((m) => {
        if (!this.buttons.find((v) => v.permissionCode === m.permissionCode)) {
          const buttonName = m.permissionName.indexOf('_') >= 0 ? m.permissionName.split('_')[1] : m.permissionName
          this.buttons.push({
            buttonName: buttonName,
            permissionCode: m.permissionCode,
            sort: this.buttons.length + 1
          })
        }
      })
    },

    /**
     * @description: 移除按钮
     * @param {*}
     */
    onRemove(index) {
      this.buttons.splice(index, 1)
    },

    /**
     * @description: 窗口打开事件
     * @param {*}
     */
    onOpened() {
      if (this.menuId) this.getData()
    }
  }
}
</script>

<style lang="scss" scoped>
.add-btn {
  padding: 10px;
}
</style>
