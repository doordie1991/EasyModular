<template>
  <em-container>
    <em-split v-model="split">
      <template v-slot:left>
        <tree-page ref="tree" @change="onTreeChange" />
      </template>
      <template v-slot:right>
        <list-page ref="list" :parent="parent" @save-success="onSaveSuccess" @remove-success="onRemoveSuccess" @move-success="onMoveSuccess"  />
      </template>
    </em-split>
  </em-container>
</template>
<script>
import page from './page'
import TreePage from '../_components/tree'
import ListPage from '../_components/list'

export default {
  name: page.name,
  components: { TreePage, ListPage },
  data() {
    return {
      split: 0.2,
      parent: {}
    }
  },
  methods: {
    /**
     * @description:刷新列表
     * @param {*}
     */
    refreshList() {
      this.$refs.list.refresh()
    },

    /**
     * @description:树节点变化
     * @param {*} id
     * @param {*} label
     * @param {*} item
     */
    onTreeChange({ id, label, item }) {
      if (item) {
        this.parent.id = id
        this.parent.name = label
        this.parent.path = item.organizeFullName
        this.refreshList()
      }
    },

    /**
     * @description:保存节点成功
     * @param {*} model
     * @param {*} data
     * @param {*} isAdd
     */
    onSaveSuccess(model, data, isAdd) {
      const nodeData = { id: model.id, label: model.organizeName, item: Object.assign({}, data) }
      if (isAdd) {
        this.$refs.tree.insert(nodeData)
      } else {
        this.$refs.tree.update(nodeData)
      }
      this.refreshList()
    },

    /**
     * @description:移除节点成功
     * @param {*} id
     */
    onRemoveSuccess(id) {
      this.$refs.tree.remove(id)
      this.refreshList()
    },

    /**
     * @description:移动节点成功
     * @param {*} id
     */
    onMoveSuccess() {
      this.$refs.tree.refresh()
    }
  }
}
</script>
