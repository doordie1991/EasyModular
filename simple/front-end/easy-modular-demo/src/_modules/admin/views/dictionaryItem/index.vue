<template>
  <em-dialog v-bind="dialog" :visible.sync="visible_" @opened="onOpened">
    <em-split class="page" v-model="split">
      <template v-slot:left>
        <tree ref="tree" :dict="dict" @change="onTreeChange" />
      </template>
      <template v-slot:right>
        <item-list ref="list" :dict="dict" :parent-id="parentId" @add-success="onAdd" @del-success="onDelete" @edit-success="onEdit" />
      </template>
    </em-split>
  </em-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'
import Tree from './_components/tree'
import ItemList from './_components/list'
export default {
  mixins: [mixins.visible],
  components: { Tree, ItemList },
  data() {
    return {
      dialog: {
        title: '数据项配置',
        icon: 'tag',
        width: '80%',
        height: '80%',
        noScrollbar: true
      },
      split: 0.2,
      parentId: this.$emptyGuid
    }
  },
  props: {
    dict: Object
  },
  methods: {
    /**
     * @description:树节点改变事件
     * @param {*} selection
     */
    onTreeChange(selection) {
      this.parentId = selection.item.id
    },

    /**
     * @description:字典项添加成功事件
     * @param {*} model
     */
    onAdd(model) {
      this.$refs.tree.insert(model)
    },

    /**
     * @description:字典项添加删除事件
     * @param {*} value
     */
    onDelete(value) {
      this.$refs.tree.remove(value)
    },

    /**
     * @description:字典项编辑成功事件
     * @param {*} model
     */
    onEdit(model) {
      this.$refs.tree.update(model)
    },

    /**
     * @description:Dialog 打开动画结束时的回调
     * @param {*}
     */
    onOpened() {
      this.parentId = this.dict.id
      this.$refs.list.refresh()
    }
  }
}
</script>
