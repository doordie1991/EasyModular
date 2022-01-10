<template>
  <div class="message-item">
    <div class="message-item-icon" :style="{ background: iconBgColor_ }">
      <em-icon :name="icon_" :color="iconColor_"></em-icon>
    </div>
    <div class="message-item-content">
      <div class="message-item-content-type">[{{ type }}]</div>
      <div class="message-item-content-txt">{{ content }}</div>
    </div>
    <div class="message-item-time">{{ time }}</div>
  </div>
</template>

<script>
export default {
  props: {
    type: String,
    content: String,
    url: String,
    time: String
  },
  computed: {
    icon_() {
      switch (this.type) {
        case '系统公告':
          return 'notification'
        case '系统待办':
          return 'edit'
        case '生日提醒':
          return 'gift'
        case '危险作业':
          return 'fire-fill'
        case '消防演习':
          return 'flag'
        default:
          return 'notification'
      }
    },
    iconColor_() {
      switch (this.type) {
        case '系统公告':
          return '#07c160'
        case '系统待办':
          return '#1989fa'
        case '生日提醒':
          return '#ff9900'
        case '危险作业':
          return '#f56c6c'
        case '消防演习':
          return '#00acc1'
        default:
          return '#00acc1'
      }
    },
    iconBgColor_() {
      switch (this.type) {
        case '系统公告':
          return 'rgba(7,193,96, 0.1)'
        case '系统待办':
          return 'rgba(25,137,250, 0.1)'
        case '生日提醒':
          return 'rgba(255,153,0, 0.1)'
        case '危险作业':
          return 'rgba(245,108,108, 0.1)'
        case '消防演习':
          return 'rgba(0,172,193, 0.1)'
        default:
          return 'rgba(0,172,193, 0.1)'
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.message-item {
  width: 100%;
  height: 64px;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 12px;
  box-sizing: border-box;
  border-bottom: 1px solid #ebeef5;

  &-icon {
    width: 44px;
    height: 44px;
    border-radius: 50%;
    margin-right: 12px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 22px;
  }

  &-content {
    flex-grow: 1;
    display: flex;
    flex-direction: column;
    justify-content: center;
    &-type {
      font-weight: bold;
      color: #303133;
    }
    &-txt {
      color: #909399;
      margin-top: 6px;
    }
  }

  &-time {
    width: 140px;
    height: 64px;
    line-height: 64px;
    text-align: center;
    color: #c0c4cc;
  }
}
</style>
