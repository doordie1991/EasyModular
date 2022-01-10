<template>
  <em-panel class="em-org" page header :loading="loading">
    <template v-slot:title>
      <el-input v-model="keywords" placeholder="请输入关键字" clearable suffix-icon="el-icon-search" :size="fontSize"></el-input>
    </template>

    <el-tree ref="tree" v-bind="tree" @node-click="onClick">
      <span slot-scope="{ data }">
        <em-icon v-if="data.item.type === 0" name="attachment" />
        <em-icon v-else-if="data.item.type === 1 && data.item.sex === true" name="boy" class="em-org-icon boy" />
        <em-icon v-else-if="data.item.type === 1 && data.item.sex === false" name="girl" class="em-org-icon girl" />
        <span :class="data.item.type === 1 ? 'em-org-name' : ''">
          {{ data.label }}
          <span class="em-size-10 em-text-info" v-if="data.item.type === 1 && data.item.jobName">{{ data.item.jobName }}</span>
        </span>
      </span>
    </el-tree>
  </em-panel>
</template>
<script>
const { getTree } = $api.admin.user

export default {
  data() {
    return {
      tree: {
        data: [],
        nodeKey: 'id',
        highlightCurrent: true,
        props: { children: 'children', label: 'label' },
        filterNodeMethod: this.filter
      },
      keywords: '',
      loading: false
    }
  },
  props: {
    //已选择的人员列表
    selection: Array
  },
  methods: {
    /**
     * @description: 获取数据
     * @param {*}
     */
    async getData() {
      this.loading = true
      const result = await getTree()
      this.tree.data = result
      this.loading = false
    },

    /**
     * @description: 查询
     * @param {*}
     */
    search() {
      this.getData()
    },

    /**
     * @description: 点击事件
     * @param {*} data
     * @param {*} checkedNodes
     */
    onClick(data) {
      if (data.item.type === 0 || this.selection.findIndex(m => m.id === data.item.id) >= 0) return
      const item = {
        id: data.id,
        userCode: data.item.code,
        userName: data.item.name,
        sex: data.item.sex,
        jobName: data.item.jobName
      }
      this.selection.push(item)
    },

    /**
     * @description: 节点过滤
     * @param {*} value
     * @param {*} data
     */
    filter(value, data) {
      if (!value) return true
      return data.label.indexOf(value) !== -1
    }
  },
  watch: {
    keywords(val) {
      this.$refs.tree.filter(val)
    }
  }
}
</script>
<style lang="scss">
.em-org {
  .el-tree-node__content {
    font-size: 12px;
  }

  .em-icon {
    margin-right: 5px;
  }
  &-icon {
    margin-left: -6px;
    font-size: 30px;

    &.boy {
      color: #409eff;
    }
    &.girl {
      color: #e6a23c;
    }
  }

  &-name {
    position: relative;
    top: -6px;
  }
}
</style>
