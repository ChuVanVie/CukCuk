<template>
    <div id="warehouse-container">
        <div class="items-center" style="padding: 8px 0 16px 0;">
            <div class="content-title">Danh mục</div>
            <div class="align-center" style=" padding: 0 8px;">
                <i class="fas fa-caret-right" style="color: #0072bc; font-size: 18px;"></i>
            </div>
            <div class="sub-title">Kho</div>
        </div>
        <div class="flex operations">
            <button class="m-btn m-btn-operate align-center" @click="showFormAddWarehouse">
                <div class="icon-add"></div>
                <p>Thêm</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-update"></div>
                <p>Sửa</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-delete"></div>
                <p>Xóa</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-reload"></div>
                <p>Nạp</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-help"></div>
                <p>Giúp</p>
            </button>
        </div>
        <div class="m-table-container">
            <GLoading v-if="isShowLoading"></GLoading>
            <table class="m-table m-table-line">
                <thead>
                    <tr>
                        <td>
                            <p>Mã kho</p>
                            <div class="search-option-container items-center">
                                <GSearchOption></GSearchOption>
                                <input type="text" class="m-input">
                            </div>
                        </td>
                        <td>
                            <p>Tên kho</p>
                            <div class="search-option-container items-center">
                                <GSearchOption></GSearchOption>
                                <input type="text" class="m-input">
                            </div>
                        </td>
                        <td>
                            <p>Địa chỉ</p>
                            <div class="search-option-container items-center">
                                <GSearchOption></GSearchOption>
                                <input type="text" class="m-input">
                            </div>
                        </td>
                        <td style="width: 125px;">
                            <p>Ngừng theo dõi</p>
                            <div class="select-input"
                                v-outside="() => this.stopfollowDropList.isShowStopFollowDropList = false">
                                <input type="text" style="width: 96px; background-color: #fff;" disabled
                                    v-model="stopfollowDropList.stopfollowValue">
                                <div class="align-center" @click="showStopFollowDropList"
                                    style="width: 22px; cursor: pointer;">
                                    <i class="fas fa-caret-down" style="color: #95a1b7;"></i>
                                </div>
                                <div class="context-menu context-menu-up" style="padding:unset; top: 24px;"
                                    v-if="stopfollowDropList.isShowStopFollowDropList">
                                    <div class="context-menu-item" style="padding-left: 4px;"
                                        v-for="(value, index) in stopfollows" :key="index" @click="chooseStopFollow(value)">
                                        <span class="item-text pg-item">{{ value }}</span>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr :class="{ 'row-active': isActiveRow[index] }" v-for="(warehouse, index) in warehouses" :key="index"
                        @click="setActive(index)" @dblclick="showFormUpdateWarehouse(warehouse)">
                        <td>{{ warehouse.warehouseCode }}</td>
                        <td>{{ warehouse.warehouseName }}</td>
                        <td>{{ warehouse.warehouseAddress }}</td>
                        <td class="text-align--center"><input type="checkbox" name="" id=""></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="pagination">
            <div class="left-pagination items-center">
                <div class="icon-first-page"></div>
                <div class="icon-prev-page"></div>
                <div style="border: 1px solid #bbb; height: 16px; margin-right: 16px"></div>
                <div class="page-number">
                    Trang <input type="text" value="1" class="m-input"> trên 1
                </div>
                <div style="border: 1px solid #bbb; height: 16px; margin: 0 16px"></div>
                <div class="icon-next-page"></div>
                <div class="icon-last-page"></div>
                <div class="icon-load-page items-center">
                    <i class="fas fa-redo"></i>
                </div>
                <div class="numberRecords-container" v-outside="() => this.isShowRecordDropList = false">
                    <input type="text" style="width: 100%; padding-left: 4px; border: unset;" disabled v-model="pageSize">
                    <div class="align-center" @click="showRecordDropList" style="width: 30px; cursor: pointer;">
                        <i class="fas fa-caret-up" style="color: #95a1b7;"></i>
                    </div>
                    <div class="context-menu context-menu-up" style="padding:unset; top: -80px;"
                        v-if="isShowRecordDropList">
                        <div class="context-menu-item" style="padding-left: 4px;" v-for="(number, index) in numberrecords"
                            :key="index" @click="chooseNumberRecords(number)">
                            <span class="item-text pg-item">{{ number }}</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="right-pagination items-center" style="margin-right: 12px;">
                Hiển thị {{ (pageNumber - 1) * pageSize + 1 }}-{{ pageNumber * pageSize }} trên {{ totalRecords }} kết quả
            </div>
        </div>
    </div>
    <WarehouseForm v-if="isShowWarehouseform" :warehouseData="warehouseSelected" :formDetail="formMode"></WarehouseForm>
</template>
<script>
import GLoading from '@/components/globalComponents/GLoading.vue';
import GSearchOption from '@/components/globalComponents/GSearchOption.vue';
import WarehouseForm from './WarehouseForm.vue';
import WarehouseService from '@/services/WarehouseService';
import resource from '@/resources/resource';
import Enum from '@/resources/enum';

const WarehouseAPI = new WarehouseService();

export default {
    name: "WarehousePage",
    components: {
        GLoading, GSearchOption, WarehouseForm
    },
    created() {
        this.getWarehousesData();
    },
    props: {
        stopfollows: {
            default: resource.stopFollow
        },
        numberrecords: {
            default: resource.numberRecords
        }
    },
    data() {
        return {
            warehouses: {},
            warehouseSelected: {},
            formMode: null,
            isActiveRow: [],
            isShowLoading: false,
            isShowRecordDropList: false,
            pageNumber: 1,
            pageSize: 25,
            totalRecords: null,
            totalPages: null,
            stopfollowDropList: {
                isShowStopFollowDropList: false,
                stopfollowValue: "Không"
            },
            isShowWarehouseform: false,
        }
    },
    methods: {
        async getWarehousesData() {
            this.isShowLoading = true;
            try {
                let res = await WarehouseAPI.getPaging(this.pageSize, this.pageNumber);
                // console.log(res);
                this.warehouses = res.data.listData;
                //Tổng số bản ghi thỏa mãn
                this.totalRecords = res.data.totalRecord;
                this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
            }
            catch (error) {
                console.log(error);
            }
            finally {
                this.isShowLoading = false;
            }
        },

        setActive(n) {
            this.isActiveRow = [];
            this.isActiveRow[n] = true;
        },

        showStopFollowDropList() {
            this.stopfollowDropList.isShowStopFollowDropList = !this.stopfollowDropList.isShowStopFollowDropList;
        },

        chooseStopFollow(value) {
            this.stopfollowDropList.stopfollowValue = value;
            this.stopfollowDropList.isShowStopFollowDropList = false;
        },

        showRecordDropList() {
            this.isShowRecordDropList = !this.isShowRecordDropList;
        },

        chooseNumberRecords(number) {
            this.pageSize = number;
            this.isShowRecordDropList = false;
        },

        showFormAddWarehouse() {
            this.formMode = Enum.FormMode.Add;
            this.isShowWarehouseform = true;
        },

        showFormUpdateWarehouse(warehouse) {
            this.formMode = Enum.FormMode.Update;
            this.warehouseSelected = warehouse;
            this.isShowWarehouseform = true;
        }

    }
}
</script>
<style scoped>
#warehouse-container {
    color: #000;
    line-height: 17px;
    height: 100%;
}

.content-title {
    font-size: 28px;
}

.sub-title {
    color: #0072bc;
    font-size: 18px;
}

.operations {
    background-color: #f2f2f2 !important;
    border: 1px solid #bbb;
    border-bottom: unset !important;
    padding: 2px;
}

.search-option-container {
    position: relative;
}

.search-option {
    width: 22px;
    height: 22px;
    margin-left: 2px;
    border: 1px solid #bbb;
    background-color: #fcfcfc !important;
    cursor: pointer;
}

.search-option:hover {
    border-color: #eb7728;
}

.row-active {
    background-color: #acd4ef;
}

.pagination {
    height: 26px;
    display: flex;
    justify-content: space-between;
}

.pagination .page-number input {
    width: 40px;
    padding: 4px;
}

.numberRecords-container {
    width: 60px;
    background-color: #fff;
    border: 1px solid #bbb;
    margin: 2px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    position: relative;
}



.icon-add {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -2648px;
    width: 14px;
    height: 16px;
    margin-right: 4px;
}

.icon-copy {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -1888px;
    width: 14px;
    height: 15px;
    margin-right: 4px;
}

.icon-update {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1872px;
    width: 15px;
    height: 15px;
    margin-right: 4px;
}

.icon-delete {
    background: url('../../assets/img/IconSprite.png') no-repeat -2px -2683px;
    width: 12px;
    height: 11px;
    margin-right: 4px;
}

.icon-reload {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -2586px;
    width: 16px;
    height: 13px;
    margin-right: 4px;
}

.icon-help {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1600px;
    width: 16px;
    height: 16px;
    margin-right: 4px;
}

.icon-first-page {
    background: url('../../assets/icon/btn-firstpage.svg') no-repeat -5px -6px;
    width: 14px;
    height: 12px;
    margin-left: 8px;
    margin-right: 16px;
}

.icon-prev-page {
    background: url('../../assets/icon/btn-prev-page.svg') no-repeat -8px -5px;
    width: 8px;
    height: 14px;
    margin-right: 16px;
}

.icon-next-page {
    background: url('../../assets/icon/btn-next-page.svg') no-repeat -8px -5px;
    width: 8px;
    height: 14px;
    margin-right: 16px;
}

.icon-last-page {
    background: url('../../assets/icon/btn-lastpage.svg') no-repeat -5px -6px;
    width: 14px;
    height: 12px;
    margin-right: 16px;
}

.icon-load-page {
    margin-right: 16px;
    cursor: pointer;
}
</style>