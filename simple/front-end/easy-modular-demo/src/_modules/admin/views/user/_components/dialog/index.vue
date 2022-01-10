<template>
  <em-dialog class="em-user-select" v-bind="dialog" :visible.sync="visible_" @opened="onOpened">
    <em-flex fix="60%" fix-mode="left" direction="row" :gutter="4">
      <template v-slot:left>
        <el-tabs class="em-user-select-tabs" v-model="tabActive" tab-position="left">
          <el-tab-pane label="通讯录" name="contacts"> <contacts-panel ref="contacts" v-show="tabActive === 'contacts'" :selection="selection"/></el-tab-pane>
          <el-tab-pane label="同组织" name="sameOrg"> <same-org-panel ref="sameOrg" v-show="tabActive === 'sameOrg'" :selection="selection"/></el-tab-pane>
          <el-tab-pane label="组织架构" name="org"> <org-panel ref="org" v-show="tabActive === 'org'" :selection="selection"/></el-tab-pane>
          <el-tab-pane label="最近选择" name="latest"> <latest-panel ref="latest" v-show="tabActive === 'latest'" :selection="selection"/></el-tab-pane>
        </el-tabs>
      </template>
      <template v-slot:right>
        <selection-panel ref="selection" :selection="selection" />
      </template>
    </em-flex>

    <template v-slot:footer>
      <em-button type="success" text="确认" @click="confirm" />
      <em-button type="warning" text="重置" @click="reset" />
      <em-button type="info" text="关闭" @click="close" />
    </template>
  </em-dialog>
</template>

<script>
import { mixins } from 'easy-modular-ui'
import ContactsPanel from './contacts-panel'
import LatestPanel from './latest-panel'
import OrgPanel from './org-panel'
import SameOrgPanel from './same-org-panel'
import SelectionPanel from './selection-panel'

const { queryByUserIds, saveLatestSelect } = $api.admin.user

export default {
  mixins: [mixins.visible],
  components: { ContactsPanel, LatestPanel, OrgPanel, SameOrgPanel, SelectionPanel },
  data() {
    return {
      dialog: {
        header: true,
        footer: true,
        title: '人员选择',
        icon: 'user',
        width: '760px',
        height: '600px',
        noScrollbar: true,
        padding: 4
      },
      tabActive: 'contacts',
      //已选择
      selection: []
    }
  },
  props: {
    //值
    value: String,
    //提示信息
    placeholder: String,
    //禁用
    disabled: Boolean,
    //选择数量限制，0表示不限制
    limit: {
      type: Number,
      default: 0
    }
  },
  methods: {
    /**
     * @description: 获取数据
     * @param {*}
     */
    async getData() {
      const params = this.value.split(',')
      const result = await queryByUserIds(params)
      this.selection = result.map(m => {
        return {
          id: m.id,
          userCode: m.userCode,
          userName: m.userName,
          sex: m.sex,
          jobName: m.jobName,
          organizeFullName: m.organizeFullName,
          checked: false,
          show: true
        }
      })
    },

    /**
     * @description: 确认
     * @param {*}
     */
    async confirm() {
      let ids = []
      if (this.selection.length > 0) {
        ids = this.selection.map(m => m.id)
        await saveLatestSelect(ids)
      }
      if (this.limit && this.limit > 0 && ids.length > this.limit) {
        this._warning(`人员选择最多不能超过${this.limit}位`)
        return
      }
      this.$emit('input', this.selection.map(v => v.id).join(','))
      this.$emit('update:label', this.selection.map(v => v.userName).join(','))
      this.$emit('change', this.selection)
      this.reset()
      this.visible_ = false
    },

    /**
     * @description: 重置
     * @param {*}
     */
    reset() {
      this.selection = []
    },

    /**
     * @description: 关闭
     * @param {*}
     */
    close() {
      this.visible_ = false
    },
    /**
     * @description: 窗口打开事件
     * @param {*}
     */
    onOpened() {
      if (this.value) this.getData()
    }
  },
  watch: {
    tabActive(val) {
      this.$nextTick(() => {
        this.$refs[val].search()
      })
    },
    value: {
      immediate: true,
      handler(val) {
        if (val) {
          this.getData()
        }
      }
    }
  }
}
</script>

<style lang="scss">
.em-user-select {
  display: flex;
  &-tabs {
    .el-tabs__item.is-top:nth-child(2) {
      padding-left: 20px;
    }
  }
  .el-tabs,
  .el-tabs__content,
  .el-tab-pane {
    height: 100%;
  }
  .el-tabs__nav-scroll {
    background: #f8f8f8;
  }
}
.em-panel-dialog > .em-panel-footer {
  justify-content: center;
}
.em-panel-header-title {
  padding: 0px 4px;
}
</style>
