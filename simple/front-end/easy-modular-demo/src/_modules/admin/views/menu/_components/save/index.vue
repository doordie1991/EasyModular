<template>
  <em-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="24">
        <el-form-item label="菜单类型" prop="menuType">
          <el-radio v-model="form.model.menuType" :label="0">节点</el-radio>
          <el-radio v-model="form.model.menuType" :label="1">路由</el-radio>
          <el-radio v-model="form.model.menuType" :label="2">链接</el-radio>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <el-form-item label="菜单名称" prop="menuName">
          <el-input v-model="form.model.menuName" clearable />
        </el-form-item>
      </el-col>
    </el-row>

    <el-row v-if="form.model.menuType === 1">
      <el-col :span="24">
        <el-form-item label="路由名称" prop="routeName" :rules="{ required: form.model.menuType === 1, message: '请输入路由名称' }">
          <el-input v-model="form.model.routeName" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row v-if="form.model.menuType === 1">
      <el-col :span="12">
        <el-form-item label="路由参数：" prop="routeParams">
          <el-input v-model="form.model.routeParams" type="textarea" :rows="2" placeholder="params"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="路由参数：" prop="routeQuery">
          <el-input v-model="form.model.routeQuery" type="textarea" :rows="2" placeholder="query"></el-input>
        </el-form-item>
      </el-col>
    </el-row>

    <el-row v-if="form.model.menuType === 2">
      <el-col :span="12">
        <el-form-item label="链接" prop="url">
          <el-input v-model="form.model.url" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="打开方式" prop="menuTarget">
          <el-radio v-model="form.model.menuTarget" :label="0">新窗口</el-radio>
          <el-radio v-model="form.model.menuTarget" :label="1">当前窗口</el-radio>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="图标：" prop="icon">
          <em-icon-picker v-model="form.model.icon" />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="图标颜色：" prop="iconColor">
          <em-color-picker v-model="form.model.iconColor" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="排序：" prop="sort">
          <el-input-number v-model="form.model.sort" />
        </el-form-item>
      </el-col>
      <el-col :span="6">
        <el-form-item label="显示：" prop="isShow">
          <el-switch v-model="form.model.isShow" />
        </el-form-item>
      </el-col>
      <el-col :span="6">
        <el-form-item label="权限控制：" prop="isControl">
          <el-switch v-model="form.model.isControl" />
        </el-form-item>
      </el-col>
    </el-row>
  </em-form-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

const { add, edit, update } = $api.admin.menu

export default {
  mixins: [mixins.formSave],
  data() {
    return {
      title: '菜单',
      actions: {
        add,
        edit,
        update
      },
      form: {
        width: '800px',
        height: '550px',
        model: {
          parentId: '',
          menuName: '',
          menuType: 0,
          menuTarget: 0,
          routeName: '',
          isShow: true,
          isControl: true,
          routeParams: '',
          routeQuery: '',
          url: '',
          icon: 'detail',
          level: 0,
          sort: 0
        },
        rules: {
          menuName: [{ required: true, message: '请输入菜单名称' }]
        }
      },
      on: {
        reset: this.onReset
      }
    }
  },
  props: ['parent', 'total'],
  methods: {
    onReset() {
      if (this.isAdd_) {
        this.form.model.parentId = this.parent.id
        this.form.model.sort = this.total
      }
    }
  }
}
</script>
