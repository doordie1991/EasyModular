<template>
  <div :class="['user-list-item', deleteBtn ? 'checked' : '']">
    <div :class="['user-list-item-avatar', data.sex ? 'boy' : 'girl']">
      <em-icon :name="data.sex ? 'boy' : 'girl'" />
    </div>
    <div class="user-list-item-info">
      <div class="user-list-item-info-name">
        {{ data.userName }}（{{ data.userCode }}）<label
          class="user-list-item-info-post"
          v-show="data.jobName"
          >{{ data.jobName }}</label
        >
      </div>
      <div class="user-list-item-info-org" v-if="data.organizeFullName">
        {{ data.organizeFullName }}
      </div>
    </div>

    <div class="user-list-item-del" v-show="deleteBtn" @click="onDel(data)">
      <em-icon name="delete"></em-icon>
    </div>

    <div class="el-icon-check" v-show="deleteBtn"></div>
  </div>
</template>

<script>
export default {
  props: {
    //数据行
    data: Object,
    //删除按钮
    deleteBtn: Boolean
  },
  methods: {
    /**
     * @description:删除事件
     * @param {*}
     */
    onDel(data) {
      this.$emit("onDelete", data)
    }
  }
}
</script>
<style lang="scss" scoped>
.user-list {
  display: flex;
  flex-direction: column;
  &-item {
    position: relative;
    display: flex;
    border-bottom: 1px solid #f2f6fc;
    cursor: pointer;
    height: 50px;

    &-avatar {
      font-size: 28px;
      width: 50px;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;

      &.boy {
        color: #409eff;
      }
      &.girl {
        color: #e6a23c;
      }
    }

    &-info {
      display: flex;
      flex-grow: 1;
      flex-direction: column;
      justify-content: center;
      &-name {
        font-weight: 600;
        font-size: 12px;
      }
      &-post {
        color: #909399;
        font-size: 10px;
        font-weight: 300;
        margin-left: 4px;
      }
      &-org {
        margin-top: 8px;
        color: #909399;
        font-size: 10px;
      }
    }
    &-del {
      width: 80px;
      font-size: 22px;
      display: flex;
      justify-content: center;
      align-items: center;
      color: #f56c6c;
    }

    .el-icon-check {
      display: none;
      position: absolute;
      top: 0;
      right: 1px;
      font-size: 11px;
      color: #fff;
      width: 0;
      height: 0;
      border-top: 25px solid #409eff;
      border-left: 25px solid transparent;

      &::before {
        position: absolute;
        top: -23px;
        left: -15px;
        font-size: 15px;
      }
    }
    &:hover,
    &.checked {
      background-color: #eaf8fe;
      border-bottom: 1px solid #e4e7ed;
    }

    &.checked {
      .el-icon-check {
        display: inline-block;
      }
    }
  }
}
</style>
