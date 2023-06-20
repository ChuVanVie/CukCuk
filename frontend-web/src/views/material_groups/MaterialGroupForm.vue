<template>
    <div class="m-mask">
        <div id="form-container">
            <div id="form-header">
                <p>Thêm Nhóm nguyên vật liệu</p>
                <div class="align-center tooltip" @click="btnCloseForm"
                    style="background-color: #fff; width: 18px; height: 18px; border-radius: 8px; cursor: pointer;">
                    <i class="fas fa-times" style="color: #0973b9;"></i>
                    <span class="tooltiptext">Đóng</span>
                </div>
            </div>
            <div id="form-content">
                <div class="form-detail">
                    <div class="textfield">
                        <div class="textfield-name">Mã nhóm <span style="color: red;">(*)</span></div>
                        <input type="text" :class="{ 'border-error': (emptyErrorsList[0] || duplicateCodeError) }"
                            class="textfield-input" v-model="materialgroup.materialGroupCode">
                        <div v-if="emptyErrorsList[0]" class="icon-warning tooltip">
                            <span class="tooltiptext">Trường này không được để trống</span>
                        </div>
                        <div v-if="duplicateCodeError" class="icon-warning tooltip">
                            <span class="tooltiptext">Mã nhóm nguyên vật liệu {{ materialgroup.materialGroupCode }} đã tồn
                                tại!</span>
                        </div>
                    </div>
                    <div class="textfield">
                        <div class="textfield-name">Tên nhóm <span style="color: red;">(*)</span></div>
                        <input type="text" :class="{ 'border-error': emptyErrorsList[1] }" class="textfield-input"
                            v-model="materialgroup.groupName">
                        <div v-if="emptyErrorsList[1]" class="icon-warning tooltip">
                            <span class="tooltiptext">Trường này không được để trống</span>
                        </div>
                    </div>

                    <div class="textfield">
                        <div class="textfield-name">Thuộc nhóm</div>
                        <GDroplist type="content" :items="properties" v-model="materialgroup.parentGroupName"
                            @setValueInput="(result) => this.materialgroup.parentGroupName = result"></GDroplist>
                    </div>
                    <div class="x-textfield flex" style="margin-bottom: 8px;">
                        <div class="textfield-name">Diễn giải</div>
                        <textarea type="text" class="x-textfield-input" v-model="materialgroup.explanation"></textarea>
                    </div>
                </div>
            </div>
            <div id="form-footer" class="space-between">
                <button class="m-btn m-btn-footer align-center" style="margin-left: 8px;">
                    <div class="icon-help"></div>
                    <p>Giúp</p>
                </button>
                <div class="flex">
                    <button class="m-btn m-btn-footer align-center" @click="btnSaveOnMaterialGroup">
                        <div class="icon-save"></div>
                        <p>Cất</p>
                    </button>
                    <button class="m-btn m-btn-footer align-center" @click="btnCloseForm">
                        <div class="icon-cancel"></div>
                        <p>Hủy bỏ</p>
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
// import resource from '@/resources/resource'
import GDroplist from '@/components/globalComponents/GDroplist.vue';
import MaterialGroupService from '@/services/MaterialGroupService';
import Enum from '@/resources/enum';

const MaterialGroupAPI = new MaterialGroupService();

export default {
    name: "MaterialGroupForm",
    components: {
        GDroplist
    },
    mounted() {
        this.generateData();
    },
    props: {
        materialgroupData: {

        },
        formDetail: {

        },
        properties: {
            default: ["Nhóm 1", "Nhóm 2", "Nhóm 3", "Nhóm 4", "Nhóm 5", "Nhóm 6", "Nhóm 7", "Nhóm 8", "Nhóm 9"]
        },
    },
    data() {
        return {
            materialgroup: {},
            emptyErrorsList: [],
            duplicateCodeError: false,
            propertyDropList: {
                isShowPropertyDropList: false,
                propertyValue: null
            },
        }
    },
    methods: {

        /**
         * Hàm Gọi API Thêm mới nhóm nguyên vật liệu
         * params: 
         * Author: CVViệt 
         * Date: 30/05/2023
         */
        async postNewMaterialGroup(materialgroupData) {
            try {
                let res = await MaterialGroupAPI.post(materialgroupData);
                this.materialgroup = res.data.data;
            } catch (error) {
                console.log(error);
                //Trường hợp mã trùng
                if (error.response.status == 409) {
                    this.duplicateCodeError = true;
                }
                //Trường hợp validateEmpty ra các trường trống/email sai
                if (error.response.status == 400) {
                    let result = error.response.data;
                    //Xử lí lỗi trả về
                    this.handleErrorAPI(result.data);
                }
            }
        },

        async updateMaterialGroup(materialgroupData) {
            try {
                await MaterialGroupAPI.put(materialgroupData.materialGroupId, materialgroupData);
            } catch (error) {
                console.log(error);
                //Trường hợp mã trùng
                if (error.response.status == 409) {
                    this.duplicateCodeError = true;
                }
                //Trường hợp validateEmpty ra các trường trống/email sai
                if (error.response.status == 400) {
                    let result = error.response.data;
                    //Xử lí lỗi trả về
                    this.handleErrorAPI(result.data);
                }
            }
        },

        generateData() {
            if (this.formDetail == Enum.FormMode.Update) {
                this.materialgroup = this.materialgroupData;
            }
        },

        validateEmpty() {
            if (this.materialgroup.materialGroupCode == null || this.materialgroup.materialGroupCode == "") {
                this.emptyErrorsList[0] = true;
            }
            else {
                this.emptyErrorsList[0] = false;
            }
            if (this.materialgroup.groupName == null || this.materialgroup.groupName == "") {
                this.emptyErrorsList[1] = true;
            }
            else {
                this.emptyErrorsList[1] = false;
            }
        },

        /**
         * Hàm xử lý button Cất, show Dialog lỗi hoặc xác nhận thêm nhân viên
         * params: 
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnSaveOnMaterialGroup: async function () {
            // console.log(this.material);
            this.validateEmpty();
            this.duplicateCodeError = false;
            //Nếu trong trường hợp thêm mới thông tin nhân viên
            if (!(this.emptyErrorsList.some(ele => ele == true))) {
                if (this.formDetail == Enum.FormMode.Add) {
                    //Gọi hàm thêm mới nhân viên (API)
                    await this.postNewMaterialGroup(this.materialgroup);
                    if (this.duplicateCodeError == false) {
                        //Đóng form thêm mới
                        this.$emit("closeForm");
                        this.$emit("setValueMaterialGroup", this.materialgroup);
                    }
                }
                //Nếu trong trường hợp cập nhật thông tin nhân viên
                else {
                    await this.updateMaterialGroup(this.materialgroup);
                    if (this.duplicateCodeError == false) {
                        //Đóng form thêm mới
                        this.$emit("closeForm");
                    }
                }
            }
        },

        showPropertyDropList() {
            this.propertyDropList.isShowPropertyDropList = !this.propertyDropList.isShowPropertyDropList;
        },
        chooseProperty(property) {
            this.propertyDropList.propertyValue = property;
            this.propertyDropList.isShowPropertyDropList = false;
        },

        btnCloseForm() {
            this.$emit("closeForm");
        }
    }
}
</script>
<style scoped>
#form-container {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #0973b9;
    width: 420px;
    height: auto;
    border-radius: 4px;
    padding: 6px;
}

#form-header {
    height: 26px;
    padding: 0 4px;
    border-color: #0973b9;
    color: #fff;
    display: flex;
    align-items: center;
    justify-content: space-between;
}

#form-content {
    height: auto;
    background-color: #fff;
    padding: 8px;
}

#form-footer {
    height: 28px;
    background-color: #fff;
    padding: 8px 0;
}

.textfield {
    display: flex;
    align-items: center;
    margin-bottom: 8px;
}

.textfield-name {
    width: 120px;
    padding-left: 6px;
}

.textfield .textfield-input {
    width: 100%;
    max-width: 190px;
    height: 24px;
    padding: 0 4px;
    border: 1px solid #bbb;
}

.x-textfield-input {
    width: 100%;
    max-width: calc(100% - 127px);
    height: 48px;
    padding: 0 4px;
    border: 1px solid #bbb;
}

.textfield .textfield-input:focus,
.x-textfield-input:focus {
    outline: unset;
    border: 1px solid #0071c1 !important;
}

.tooltip {
    position: relative;
    display: inline-block;
    z-index: 99;
}

.tooltip .tooltiptext {
    visibility: hidden;
    width: max-content;
    font-size: 12px;
    background-color: black;
    color: #fff;
    text-align: center;
    border-radius: 4px;
    padding: 5px;

    /* Position the tooltip */
    position: absolute;
    top: -50%;
    left: 100%;
}

.tooltip:hover .tooltiptext {
    visibility: visible;
}

.icon-warning {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1808px;
    width: 16px;
    height: 16px;
    margin-left: 4px;
}

.icon-help {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1600px;
    width: 16px;
    height: 16px;
    margin-right: 4px;
}

.icon-save {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -768px;
    width: 14px;
    height: 15px;
    margin-right: 4px;
}

.icon-cancel {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1968px;
    width: 15px;
    height: 15px;
    margin-right: 4px;
}
</style>