<template>
  <div class="conditional-type-select">
    <el-select v-model="value_" placeholder="请选择" :size="fontSize">
      <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value"> </el-option>
    </el-select>
  </div>
</template>

<script>
export default {
  props: { value: Number },
  data() {
    return {
      value_: null,
      options: [
        {
          value: 0,
          label: '等于'
        },
        {
          value: 1,
          label: '模糊查询'
        },
        {
          value: 2,
          label: '大于'
        },
        {
          value: 3,
          label: '大于等于'
        },
        {
          value: 4,
          label: '小于'
        },
        {
          value: 5,
          label: '小于等于'
        },
        {
          value: 6,
          label: '包含'
        },
        {
          value: 7,
          label: '不包含'
        },
        {
          value: 8,
          label: '左模糊'
        },
        {
          value: 9,
          label: '右模糊'
        },
        {
          value: 10,
          label: '不等于'
        },
        {
          value: 11,
          label: '是null或者空'
        },
        {
          value: 12,
          label: '不等于null'
        }
      ]
    }
  },
  watch: {
    value: {
      handler(n, o) {
        if (n != this.value_) this.value_ = n
      },
      immediate: true
    },
    value_(n, o) {
      if (n != o) this.$emit('input', n)
    }
  }
}
</script>
