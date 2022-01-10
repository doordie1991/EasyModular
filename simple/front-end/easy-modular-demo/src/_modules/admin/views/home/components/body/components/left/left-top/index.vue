<template>
  <em-panel class="em-home-body-left-top" v-bind="panel">
    <template v-slot:toolbar>
      <el-button-group>
        <el-button size="mini" plain @click="btnClick(1)">今日</el-button>
        <el-button size="mini" plain @click="btnClick(2)">本周</el-button>
        <el-button size="mini" plain @click="btnClick(3)">本月</el-button>
        <el-button size="mini" plain @click="btnClick(4)">全年</el-button> </el-button-group
      >&nbsp;&nbsp;
      <el-date-picker
        size="mini"
        v-model="date"
        valueFormat="yyyy-MM-dd"
        style="width: 221px !important"
        type="daterange"
        range-separator="-"
        start-placeholder="开始日期"
        end-placeholder="结束日期"
      ></el-date-picker>
    </template>
    <ve-line :data="chartData" :settings="chartSettings" :extend="chartExtend" height="100%"> </ve-line>
  </em-panel>
</template>

<script>
export default {
  data() {
    return {
      panel: {
        title: '事件快报',
        icon: 'alert-fill',
        iconColor: '#00acc1',
        height: '320px',
        header: true,
        noScrollbar: true
      },
      date: [],
      chartData: {
        columns: ['date', 'amount'],
        rows: []
      },
      chartSettings: {
        labelMap: {
          date: '日期',
          amount: '提报数量'
        },
        itemStyle: {
          normal: {
            color: '#3dd5e9',
            borderColor: '#ebeef5', //拐点边框颜色
            borderWidth: 8 //拐点边框大小
          },
          emphasis: {
            color: '#3dd5e9' //hover拐点颜色定义
          }
        },
        lineStyle: {
          color: {
            type: 'linear',
            x: 0,
            y: 0,
            x2: 0,
            y2: 1,
            colorStops: [
              {
                offset: 0,
                color: '#f8bd65'
              },
              {
                offset: 1,
                color: '#3dd5e9'
              }
            ]
          },
          width: 4
        }
      },
      chartExtend: {
        legend: {
          show: false
        },

        grid: {
          top:24,
          bottom: 12
        },
        xAxis: {
          axisLabel: {
            textStyle: {
              color: '#909399'
            }
          }
        },
        yAxis: {
          axisLabel: {
            textStyle: {
              color: '#c0c4cc'
            }
          },
          splitLine: {
            show: true,
            lineStyle: {
              color: ['#e4e7ed'],
              width: 1
            }
          }
        }
      }
    }
  },
  methods: {
    getData() {
      this.chartData.rows = []
      for (let index = 1; index < 31; index++) {
        this.chartData.rows.push({ date: index, amount: Math.floor(Math.random() * (100 - 200)) + 100 })
      }
    },
    refresh() {
      this.getData()
    },
    btnClick(type) {
      this.date = []
      let day = this.$dayjs()
      let weekNumber = parseInt(this.$dayjs().format('d'))
      let firstDay = ''
      let lastDay = ''
      switch (type) {
        // 今日
        case 1:
          firstDay = day
          lastDay = day
          break
        // 本周
        case 2:
          if (weekNumber > 0) {
            firstDay = this.$dayjs().add(1 - weekNumber, 'day')
            lastDay = this.$dayjs().add(7 - weekNumber, 'day')
          } else {
            firstDay = this.$dayjs().add(-6, 'day')
            lastDay = this.$dayjs()
          }
          break
        // 本月
        case 3:
          firstDay = this.$dayjs().startOf('month')
          lastDay = this.$dayjs().endOf('month')

          break
        // 全年
        case 4:
          firstDay = this.$dayjs().startOf('year')
          lastDay = this.$dayjs().endOf('year')
          break
      }
      this.date.push(this.$dayjs(firstDay).format('YYYY-MM-DD'))
      this.date.push(this.$dayjs(lastDay).format('YYYY-MM-DD'))
    }
  },
  created() {
    this.getData()
  }
}
</script>
