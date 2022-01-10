<template>
  <div class="user-select">
    <el-input v-model="label_" :placeholder="placeholder" class="input-with-select" disabled>
      <el-button slot="append" icon="el-icon-user" @click="dialogShow = true"></el-button>
    </el-input>
    <!--用户选择窗口-->
    <user-select-dialog :visible.sync="dialogShow" @change="onSelect" v-model="value_"></user-select-dialog>
  </div>
</template>

<script>
import { mixins } from 'easy-modular-ui'
import UserSelectDialog from '../dialog/index'
const { queryByUserIds } = $api.admin.user
export default {
  mixins: [mixins.visible],
  components: { UserSelectDialog },
  data() {
    return {
      value_: '',
      label_: '',
      dialogShow: false
    }
  },
  props: {
    value: String,
    label: String,
    placeholder: {
      type: String,
      default: '请选择'
    }
  },
  methods: {
    /**
     * @description: 选择事件
     * @param {*} rows
     */
    onSelect(rows) {
      this.value_ = rows.map(m => m.id).join(',')
      this.label_ = rows.map(m => m.userName).join(',')
    },
    /**
     * @description:设置label的值
     * @param {*}
     */
    async setLabel() {
      const params = this.value.split(',')
      const result = await queryByUserIds(params)
      this.label_ = result.map(m => m.userName).join(',')
    }
  },
  watch: {
    value: {
      immediate: true,
      handler(val) {
        if (this.value_ != val) this.value_ = val
      }
    },
    label: {
      immediate: true,
      handler(val) {
        if (this.label_ != val) this.label_ = val
      }
    },
    value_(val) {
      if (val != this.value) this.$emit('input', val)
    },
    label_(val) {
      if (val != this.label) this.$emit('update:label', val)
    }
  },
  mounted() {
    if (this.value && !this.label) this.setLabel()
  }
}
</script>
