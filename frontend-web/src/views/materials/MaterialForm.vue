<template>
    <div class="m-mask" @keydown.esc="btnCloseForm" @keydown.ctrl.s.prevent="btnSaveOnClick">
        <div id="form-container">
            <div id="form-header">
                <p>Thêm Nguyên vật liệu</p>
                <div class="align-center tooltip" @click="btnCloseForm"
                    style="background-color: #fff; width: 18px; height: 18px; border-radius: 8px; cursor: pointer;">
                    <i class="fas fa-times" style="color: #0973b9;"></i>
                    <span class="tooltiptext">Đóng</span>
                </div>
            </div>
            <div id="form-content">
                <div class="form-detail">
                    <div class="flex" style="margin-bottom: 8px;">
                        <div class="textfield">
                            <div class="textfield-name">Mã <span style="color: red;">(*)</span></div>
                            <input type="text" class="textfield-input"
                                :class="{ 'border-error': (emptyErrorsList[0] || duplicateCodeError) }"
                                style="width: 190px;" ref="firstfocus" v-model="material.materialCode">
                            <div v-if="emptyErrorsList[0]" class="icon-warning tooltip">
                                <span class="tooltiptext">Trường này không được để trống</span>
                            </div>
                            <div v-if="duplicateCodeError" class="icon-warning tooltip">
                                <span class="tooltiptext">Mã nguyên vật liệu {{ material.materialCode }} đã tồn tại!</span>
                            </div>
                        </div>
                        <div class="textfield">
                            <div class="textfield-name">Tên <span style="color: red;">(*)</span></div>
                            <input type="text" class="textfield-input" :class="{ 'border-error': emptyErrorsList[1] }"
                                v-model="material.materialName">
                            <div v-if="emptyErrorsList[1]" class="icon-warning tooltip">
                                <span class="tooltiptext">Trường này không được để trống</span>
                            </div>
                        </div>
                    </div>
                    <div class="flex" style="margin-bottom: 8px;">
                        <div class="textfield">
                            <div class="textfield-name">Tính chất <span style="color: red;">(*)</span></div>
                            <GDropList type="content" :items="properties" :emptyError="emptyErrorsList[2]"
                                v-model="material.materialProperty"
                                @setValueInput="(result) => this.material.materialProperty = result"></GDropList>
                            <div v-if="emptyErrorsList[2]" class="icon-warning tooltip">
                                <span class="tooltiptext">Trường này không được để trống</span>
                            </div>
                        </div>
                        <div class="textfield">
                            <div class="textfield-name">Nhóm NVL</div>
                            <GDropList type="y-content" :items="materialgroupArray" v-model="material.groupName"
                                @setValueInput="(result) => { this.material.materialGroupId = result.key; this.material.groupName = result.value }"
                                @showFormAdd="this.isShowMaterialGroupForm = true">
                            </GDropList>
                        </div>
                    </div>
                    <div class="flex" style="margin-bottom: 8px;">
                        <div class="textfield">
                            <div class="textfield-name">ĐVT <span style="color: red;">(*)</span></div>
                            <GDropList type="y-content" :items="calculationunitArray" :emptyError="emptyErrorsList[3]"
                                v-model="material.calculationUnitName" @setValueInput="chooseMaterialUnit"
                                @showFormAdd="this.isShowUnitForm = true"></GDropList>
                            <div v-if="emptyErrorsList[3]" class="icon-warning tooltip">
                                <span class="tooltiptext">Trường này không được để trống</span>
                            </div>
                        </div>
                        <div class="textfield">
                            <div class="textfield-name">Kho ngầm định</div>
                            <GDropList type="y-content" :items="warehouseArray" v-model="material.warehouseName"
                                @setValueInput="(result) => { this.material.warehouseId = result.key; this.material.warehouseName = result.value }"
                                @showFormAdd="this.isShowWarehouseForm = true"></GDropList>
                        </div>
                    </div>
                    <div class="flex" style="margin-bottom: 8px;">
                        <div class="textfield">
                            <div class="textfield-name">Hạn sử dụng</div>
                            <input type="number" min="0" class="textfield-input text-align--right" v-model="material.expiry"
                                style="width: 88px; margin-right: 12px;">
                            <div class="select-input" v-outside="() => this.isShowExpiryDropList = false">
                                <input type="text" style="width: 80px;" v-model="material.expiryText">
                                <div class="align-center" @click="showExpiryDropList" style="width: 22px; cursor: pointer;">
                                    <i class="fas fa-caret-down" style="color: #95a1b7;"></i>
                                </div>
                                <div class="context-menu context-menu-up" style="padding:unset; top: 24px;"
                                    v-if="isShowExpiryDropList">
                                    <div class="context-menu-item" style="padding-left: 4px;"
                                        v-for="(expiry, index) in expiries" :key="index" @click="chooseExpiry(expiry)">
                                        <div class="item-text pg-item">{{ expiry }}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="textfield">
                            <div class="textfield-name">SL tồn tối thiểu</div>
                            <input type="number" min="0" step="any" class="textfield-input text-align--right"
                                style="width: 88px;" v-model="material.minimumStock">
                        </div>
                    </div>
                    <div class="x-textfield flex" style="margin-bottom: 8px;">
                        <div class="textfield-name">Ghi chú</div>
                        <textarea class="x-textfield-input" v-model="material.note"></textarea>
                    </div>
                    <div class="items-center" style="margin-bottom: 4px;">
                        <p style="width: 120px;padding-left: 8px;">Ngừng theo dõi</p>
                        <input type="checkbox" v-model="material.stopFollowing">
                    </div>
                </div>
                <div class="flex operations" style="height: auto;padding: unset;">
                    <button class="x-form-btn x-form-btn--active align-center">
                        <p>Đơn vị chuyển đổi</p>
                    </button>
                </div>
                <div class="table-container">
                    <table class="m-table m-table-convertion">
                        <thead>
                            <tr>
                                <td class="text-align--center">STT</td>
                                <td class="text-align--center">Đơn vị chuyển đổi</td>
                                <td class="text-align--center">Tỷ lệ chuyển đổi</td>
                                <td class="text-align--center">Phép tính</td>
                                <td class="text-align--center">Mô tả</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="i in convertions" :key="i">
                                <td class="text-align--center" style="width: 40px; background-color: #f2f2f2;">
                                    {{ i }}
                                </td>
                                <td>
                                    <GDropList type="y-content" :items="calculationunitArray"
                                        v-model="unitConvertions[i - 1].calculationUnitName"
                                        @setValueInput="(result) => chooseConvertionUnit(result, i)"
                                        @showFormAdd="this.isShowUnitForm = true"></GDropList>
                                </td>
                                <td>
                                    <input type="number" min="1" class="convertion-input text-align--right"
                                        v-model="unitConvertions[i - 1].scale" @input="autoGenerateDescription(i - 1)">
                                </td>
                                <td>
                                    <GDropList type="content" :items="calculations"
                                        v-model="unitConvertions[i - 1].calculate"
                                        @setValueInput="(result) => { this.unitConvertions[i - 1].calculate = result[0]; autoGenerateDescription(i - 1) }">
                                    </GDropList>
                                </td>
                                <td><input type="text" disabled v-model="unitConvertions[i - 1].description"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="flex" style="margin-top: 4px;">
                    <button class="m-btn m-btn-footer align-center" style="margin-right: 4px; margin-bottom: 4px;"
                        @click="addNewRowConvertion">
                        <div class="icon-add"></div>
                        <p>Thêm dòng</p>
                    </button>
                    <button class="m-btn m-btn-footer align-center" style=" margin-bottom: 4px;"
                        @click="deleteRowConvertion">
                        <div class="icon-delete"></div>
                        <p>Xóa dòng</p>
                    </button>
                </div>
            </div>
            <div id="form-footer" class="space-between">
                <button class="m-btn m-btn-footer align-center" style="margin-left: 8px;">
                    <div class="icon-help"></div>
                    <p>Giúp</p>
                </button>
                <div class="flex">
                    <button class="m-btn m-btn-footer align-center" @click="btnSaveOnClick">
                        <div class="icon-save"></div>
                        <p>Cất</p>
                    </button>
                    <button class="m-btn m-btn-footer align-center" @click="btnSaveAndRenew">
                        <div class="icon-save-renew"></div>
                        <p>Cất & Thêm</p>
                    </button>
                    <button class="m-btn m-btn-footer align-center" @click="btnCloseForm">
                        <div class="icon-cancel"></div>
                        <p>Hủy bỏ</p>
                    </button>
                </div>
            </div>
            <MaterialGroupForm v-if="isShowMaterialGroupForm" :formDetail="formAdd"
                @setValueMaterialGroup="addNewMaterialGroup" @closeForm="this.isShowMaterialGroupForm = false">
            </MaterialGroupForm>
            <WarehouseForm v-if="isShowWarehouseForm" :formDetail="formAdd" @setValueWarehouse="addNewWarehouse"
                @closeForm="this.isShowWarehouseForm = false"></WarehouseForm>
            <CalculationUnitForm v-if="isShowUnitForm" :formDetail="formAdd" @setValueUnit="addNewUnit"
                @closeForm="this.isShowUnitForm = false"></CalculationUnitForm>
        </div>
    </div>
    <GDialog v-if="gdialog.isShowDialog" :type="gdialog.typeDialog" @clickAgree="btnAgreeUpdate"
        @clickCancel="btnCancelUpdate" @clickDisable="btnDisable" @clickOK="btnDisable"></GDialog>
    <GDialogErrorApi v-if="isShowDialogErrorApi" @clickAgree="this.isShowDialogErrorApi = false"></GDialogErrorApi>
</template>
<script>
import GDropList from '@/components/globalComponents/GDroplist.vue';
import GDialog from '@/components/globalComponents/GDialog.vue';
import GDialogErrorApi from '@/components/globalComponents/GDialogErrorAPI.vue';
import MaterialService from '@/services/MaterialService';
import MaterialGroupService from '@/services/MaterialGroupService';
import WarehouseService from '@/services/WarehouseService';
import CalculationUnitService from '@/services/CalculationUnit';
import UnitConvertionService from '@/services/UnitConvertionService';
import MaterialGroupForm from '@/views/material_groups/MaterialGroupForm.vue';
import WarehouseForm from '../warehouses/WarehouseForm.vue';
import CalculationUnitForm from '../calculation_units/CalculationUnitForm.vue';
import resource from '@/resources/resource';
import Enum from '@/resources/enum';

const MaterialAPI = new MaterialService();
const MaterialGroupAPI = new MaterialGroupService();
const WarehouseAPI = new WarehouseService();
const CalculationUnitAPI = new CalculationUnitService();
const UnitConvertionAPI = new UnitConvertionService();

export default {
    name: "MaterialForm",
    components: {
        GDropList, GDialog, GDialogErrorApi, MaterialGroupForm, WarehouseForm, CalculationUnitForm
    },
    created() {
        this.initData();
    },

    mounted() {
        this.$refs.firstfocus.focus();

    },

    props: {
        materialData: {

        },
        formAdd: {
            default: Enum.FormMode.Add
        },
        formDetail: {

        },
        properties: {
            default: resource.materials.properties
        },
        expiries: {
            default: resource.materials.expiry
        },
        calculations: {
            default: ["* - nhân", "/ - chia"]
        }
    },
    data() {
        return {
            material: {},
            materialJSON: {},
            materialgroups: {},
            materialgroupArray: [],
            warehouses: {},
            warehouseArray: [],
            calculationunits: {},
            calculationunitArray: [],
            unitConvertions: [],
            convertions: 0,
            arrayDeleteConvertion: [],
            emptyErrorsList: [],
            duplicateCodeError: false,
            isShowExpiryDropList: false,
            isShowMaterialGroupForm: false,
            isShowWarehouseForm: false,
            isShowUnitForm: false,
            gdialog: {
                isShowDialog: false,
                typeDialog: null
            },
            isShowDialogErrorApi: false
        }
    },
    methods: {
        /**
         * Hàm gọi API lấy tất cả các bản ghi nhóm NVL
         * params: ()
         * Author: CVViệt ()
         * Date: 30/5/2023
         */
        getMaterialGroupsData() {
            try {
                return MaterialGroupAPI.getAll();
            }
            catch (error) {
                this.isShowDialogErrorApi = true;
            }
        },

        /**
         * Hàm gọi API lấy tất cả các bản ghi kho
         * params: ()
         * Author: CVViệt ()
         * Date: 30/5/2023
         */
        getWarehousesData() {
            try {
                return WarehouseAPI.getAll();
            }
            catch (error) {
                this.isShowDialogErrorApi = true;
            }
        },

        /**
         * Hàm gọi API lấy tất cả các bản ghi đơn vị tính
         * params: ()
         * Author: CVViệt ()
         * Date: 30/5/2023
         */
        getCalculationUnitsData() {
            try {
                return CalculationUnitAPI.getAll();
            }
            catch (error) {
                this.isShowDialogErrorApi = true;
            }
        },

        getUnitConvertionByMaterial(materialId) {
            try {
                return UnitConvertionAPI.getById(materialId);
            } catch (error) {
                this.isShowDialogErrorApi = true;
            }
        },


        /**
         * Hàm thêm mới đơn vị chuyển đổi
         * params: ()
         * Author: CVViệt ()
         * Date: 06/06/2023
         */
        async postNewUnitConvertion(convertion) {
            try {
                await UnitConvertionAPI.post(convertion);
            } catch (error) {
                console.log(error);
                //Trường hợp mã trùng
                if (error.response.status == 409) {
                    let result = error.response.data;
                    //Xử lí lỗi trả về
                    this.handleErrorAPI(result.data);
                }
                //Trường hợp validateEmpty ra các trường trống/email sai
                if (error.response.status == 400) {
                    let result = error.response.data;
                    //Xử lí lỗi trả về
                    this.handleErrorAPI(result.data);
                }
            }
        },

        /**
         * Hàm cập nhật đơn vị chuyển đổi
         * params: ()
         * Author: CVViệt ()
         * Date: ()
         */
        async updateUnitConvertion(convertion) {
            try {
                await UnitConvertionAPI.put(convertion.unitConvertionId, convertion);
            } catch (error) {
                console.log(error);
                //Trường hợp mã trùng
                if (error.response.status == 409) {
                    let result = error.response.data;
                    //Xử lí lỗi trả về
                    this.handleErrorAPI(result.data);
                }
                //Trường hợp validateEmpty ra các trường trống/email sai
                if (error.response.status == 400) {
                    let result = error.response.data;
                    //Xử lí lỗi trả về
                    this.handleErrorAPI(result.data);
                }
            }
        },

        async deleteUnitConvertion(convertionId) {
            try {
                await UnitConvertionAPI.delete(convertionId);
            }
            catch (error) {
                this.isShowDialogErrorApi = true;
            }
        },

        /**
         * Hàm lấy mã nguyên vật liệu mới từ API
         * params: ()
         * Author: CVViệt ()
         * Date: ()
         */
        getNewMaterialCode() {
            try {
                return MaterialAPI.getNewCode();
            }
            catch (error) {
                this.isShowDialogErrorApi = true;
            }
        },

        /**
         * Hàm Gọi API Thêm mới nguyên vật liệu
         * params: 
         * Author: CVViệt 
         * Date: 30/05/2023
         */
        async postNewMaterial(materialData) {
            try {
                await MaterialAPI.post(materialData);
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

        /**
         * Hàm Gọi API cập nhật thông tin nguyên vật liệu
         * params: 
         * Author: CVViệt 
         * Date: 30/05/2023
         */
        async updateDataMaterial(materialData) {
            try {
                await MaterialAPI.put(materialData.materialId, materialData);
            } catch (error) {
                // console.log(error);
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


        /**
         * Hàm khởi tạo các giá trị của nhóm NVL khi mở form
         * params: ()
         * Author: CVViệt ()
         * Date: 03/06/2023
         */
        async initMaterialGroupData() {
            let res = await this.getMaterialGroupsData();
            this.materialgroups = res.data;
            for (let i = 0; i < res.data.length; i++) {
                //Mảng chứa id và name của nhóm NVL truyền vào droplist
                this.materialgroupArray.push(new Object({
                    key: res.data[i].materialGroupId,
                    value: res.data[i].groupName
                }));
            }
        },

        /**
         * Hàm khởi tạo các giá trị của kho khi mở form
         * params: ()
         * Author: CVViệt ()
         * Date: 03/06/2023
         */
        async initWarehouseData() {
            let res = await this.getWarehousesData();
            this.warehouses = res.data;
            for (let i = 0; i < res.data.length; i++) {
                //Mảng chứa id và name của kho truyền vào droplist
                this.warehouseArray.push(new Object({
                    key: res.data[i].warehouseId,
                    value: res.data[i].warehouseName
                }));
            }
        },

        /**
         * Hàm khởi tạo các giá trị của đơn vị tính khi mở form
         * params: ()
         * Author: CVViệt ()
         * Date: 03/06/2023
         */
        async initCalculationUnitData() {
            let res = await this.getCalculationUnitsData();
            this.calculationunits = res.data;
            for (let i = 0; i < res.data.length; i++) {
                //Mảng chứa id và name của ĐVT truyền vào droplist
                this.calculationunitArray.push(new Object({
                    key: res.data[i].calculationUnitId,
                    value: res.data[i].calculationUnitName
                }));
            }
        },

        /**
         * Hàm lấy mã NVL mới và khởi tạo các giá trị mặc định của form
         * params: ()
         * Author: CVViệt ()
         * Date: 03/06/2023
         */
        async getNewCode() {
            let resNewMaterialCode = await this.getNewMaterialCode();
            this.material.materialId = resNewMaterialCode.data.newMaterialId;
            this.material.materialCode = resNewMaterialCode.data.newMaterialCode;
            this.material.materialProperty = "Nguyên vật liệu";
            this.material.expiry = 0;
            this.material.expiryText = "Ngày";
            this.material.minimumStock = 0;
        },

        /**
         * Hàm khởi tạo các giá trị của form khi đc created
         * params: ()
         * Author: CVViệt ()
         * Date: 03/06/2023
         */
        async initData() {
            this.initMaterialGroupData();
            this.initWarehouseData();
            this.initCalculationUnitData();
            if (this.formDetail == Enum.FormMode.Add) {
                this.getNewCode();
            }
            else {
                //Form update
                this.materialJSON = JSON.stringify(this.materialData);
                this.material = JSON.parse(this.materialJSON);
                this.material.stopFollowing = this.material.stopFollowing == 1 ? true : false;
                let res = await this.getUnitConvertionByMaterial(this.material.materialId);
                this.unitConvertions = res.data;
                if (this.unitConvertions) {
                    this.convertions = this.unitConvertions.length;
                    this.unitConvertions.forEach((item, index) => { this.unitConvertions[index].calculationUnitName = item.calculationUnitName })
                }
            }
        },

        handleErrorAPI() {

        },

        /**
         * Hàm validate các trường bắt buộc bị trống
         * params: ()
         * Author: CVViệt ()
         * Date: 03/06/2023
         */
        validateEmpty() {
            //Mã NVL trống
            if (this.material.materialCode == null || this.material.materialCode == "") {
                this.emptyErrorsList[0] = true;
            }
            else {
                this.emptyErrorsList[0] = false;
            }
            //Tên NVL trống
            if (this.material.materialName == null || this.material.materialName == "") {
                this.emptyErrorsList[1] = true;
            }
            else {
                this.emptyErrorsList[1] = false;
            }
            //Tính chất trông
            if (this.material.materialProperty == null || this.material.materialProperty == "") {
                this.emptyErrorsList[2] = true;
            }
            else {
                this.emptyErrorsList[2] = false;
            }
            //ĐVT trống
            if (this.material.calculationUnitName == null || this.material.calculationUnitName == "") {
                this.emptyErrorsList[3] = true;
            }
            else {
                this.emptyErrorsList[3] = false;
            }
        },

        /**
         * Hàm click đóng form 
         * params: ()
         * Author: CVViệt ()
         * Date: 03/06/2023
         */
        btnCloseForm() {
            if (this.formDetail == Enum.FormMode.Add) {
                this.$emit("closeForm");
                this.$emit("reload");
            }
            else {
                // console.log(this.material, this.materialData, this.material === this.materialData);
                this.material.stopFollowing = this.material.stopFollowing ? 1 : 0;
                if (JSON.stringify(this.material) != this.materialJSON) {
                    this.gdialog.typeDialog = 'update';
                    this.gdialog.isShowDialog = true;
                }
                else {
                    this.gdialog.isShowDialog = false;
                    this.$emit("closeForm");
                }
            }
        },


        /**
         * Hàm chọn đơn vị tính của form nguyên vật liệu
         * params: ()
         * Author: CVViệt ()
         * Date: 06/06/2023
         */
        chooseMaterialUnit(result) {
            this.material.calculationUnitId = result.key;
            this.material.calculationUnitName = result.value;
            //Kiểm tra đơn vị chính trùng đơn vị chuyển đổi không
            if (this.convertions > 0) {
                for (let i = 0; i < this.convertions; i++) {
                    if (this.unitConvertions[i].calculationUnitName != null && this.unitConvertions[i].calculationUnitName != "") {
                        //Trùng đơn vị chuyển đổi
                        if (this.material.calculationUnitName == this.unitConvertions[i].calculationUnitName) {
                            this.gdialog.typeDialog = 'warning';
                            this.gdialog.isShowDialog = true;
                            this.material.calculationUnitName = "";
                        }
                        else {
                            this.autoGenerateDescription(i);
                        }
                    }
                }
            }
        },

        /**
         * Hàm thêm mới nhóm NVL thành công
         * params: ()
         * Author: CVViệt ()
         * Date: 05/06/2023
         */
        async addNewMaterialGroup(materialgroup) {
            this.material.materialGroupId = materialgroup.materialGroupId;
            this.material.groupName = materialgroup.groupName;
            this.isShowMaterialGroupForm = false;
        },

        /**
         * Hàm thêm mới kho thành công
         * params: ()
         * Author: CVViệt ()
         * Date: 05/06/2023
         */
        addNewWarehouse(warehouse) {
            this.material.warehouseId = warehouse.warehouseId;
            this.material.warehouseName = warehouse.warehouseName;
            this.isShowWarehouseForm = false;
        },

        /**
         * Hàm thêm mới đơn vị tính thành công
         * params: ()
         * Author: CVViệt ()
         * Date: 05/06/2023
         */
        addNewUnit(unit) {
            this.material.calculationUnitId = unit.calculationUnitId;
            this.material.calculationUnitName = unit.calculationUnitName;
            this.isShowUnitForm = false;
        },

        /**
         * Hàm thêm mới các đơn vị chuyển đổi
         * params: ()
         * Author: CVViệt ()
         * Date: 06/06/2023
         */
        postConvertion: async function () {
            await this.unitConvertions.forEach((convertion) => {
                if (convertion.calculationUnitId) {
                    convertion.materialId = this.material.materialId;
                    this.postNewUnitConvertion(convertion);
                }
            })
        },

        /**
         * Hàm cập nhật các đơn vị chuyển đổi(thêm, sửa, xóa)
         * params: ()
         * Author: CVViệt ()
         * Date: 06/06/2023
         */
        updateConvertion: async function () {
            this.unitConvertions.forEach((convertion) => {
                if (convertion.calculationUnitId) {
                    //Gọi API thêm các đơn vị chuyển đổi mới 
                    if (!convertion.unitConvertionId) {
                        convertion.materialId = this.material.materialId;
                        this.postNewUnitConvertion(convertion);
                    }
                    //Gọi API sửa các đơn vị chuyển đổi đã có
                    else {
                        this.updateUnitConvertion(convertion);
                    }
                }
            })
            //Gọi API xóa các đơn vị chuyển đổi đã xóa
            this.arrayDeleteConvertion.forEach((convertion) => {
                this.deleteUnitConvertion(convertion.unitConvertionId);
            });
            this.arrayDeleteConvertion = [];
        },

        /**
         * Hàm xử lý button Cất, show Dialog lỗi hoặc xác nhận thêm nguyên vật liệu
         * params: 
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnSaveOnClick: async function () {
            // console.log(this.material);
            this.validateEmpty();
            this.duplicateCodeError = false;
            //Nếu trong trường hợp thêm mới thông tin nhân viên
            if (!(this.emptyErrorsList.some(ele => ele == true))) {
                this.material.stopFollowing = this.material.stopFollowing ? 1 : 0;
                if (this.formDetail == Enum.FormMode.Add) {
                    //Gọi hàm thêm mới nhân viên (API)
                    await this.postNewMaterial(this.material);
                    if (this.duplicateCodeError == false) {
                        this.postConvertion();
                        //Đóng form thêm mới
                        this.$emit("closeForm");
                        //Load lại dữ liệu
                        this.$emit("reload");
                        //Hiển thị toast thông báo thêm mới thành công
                        this.$emit("onShowToast");
                    }
                }
                //Nếu trong trường hợp cập nhật thông tin nhân viên
                else {
                    await this.updateDataMaterial(this.material);
                    if (this.duplicateCodeError == false) {
                        this.updateConvertion();
                        //Đóng form thêm mới
                        this.$emit("closeForm");
                        //Load lại dữ liệu
                        this.$emit("reload");
                        //Hiển thị toast thông báo thêm mới thành công
                        this.$emit("onShowToast");
                    }
                }
            }
        },

        /**
         * Hàm xử lý button Cất và thêm, show Dialog lỗi hoặc xác nhận thêm nhân viên
         * params: 
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnSaveAndRenew: async function () {
            this.validateEmpty();
            this.duplicateCodeError = false;
            //Nếu trong trường hợp thêm mới thông tin nhân viên
            if (!(this.emptyErrorsList.some(ele => ele == true))) {
                if (this.formDetail == Enum.FormMode.Add) {
                    //Gọi hàm thêm mới nhân viên (API)
                    await this.postNewMaterial(this.material);
                    if (this.duplicateCodeError == false) {
                        //Post các đơn vị chuyển đổi của nguyên vật liệu
                        this.postConvertion();
                        //Đóng form thêm mới
                        this.$emit("closeForm");
                        //Hiển thị toast thông báo thêm mới thành công
                        this.$emit("onShowToast");
                        this.material = {};
                        this.$refs.firstfocus.focus();
                        this.getNewCode();
                        this.$emit("openNewForm");
                    }
                }
                //Nếu trong trường hợp cập nhật thông tin nhân viên
                else {
                    await this.updateDataMaterial(this.material);
                    if (this.duplicateCodeError == false) {
                        //Update các đơn vị chuyển đổi của nguyên vật liệu
                        this.updateConvertion();
                        //Đóng form thêm mới
                        this.$emit("closeForm");
                        //Load lại dữ liệu
                        this.$emit("reload");
                        //Hiển thị toast thông báo thêm mới thành công
                        this.$emit("onShowToast");
                    }
                }
            }
        },

        /**
         * Hàm click Đồng ý cập nhật thông tin NVL
         * params: ()
         * Author: CVViệt ()
         * Date: 01/06/2023
         */
        async btnAgreeUpdate() {
            await this.updateDataMaterial(this.material);
            if (this.duplicateCodeError == false) {
                this.gdialog.isShowDialog = false;
                //Đóng form thêm mới
                this.$emit("closeForm");
                //Load lại dữ liệu
                this.$emit("reload");
                //Hiển thị toast thông báo thêm mới thành công
                this.$emit("onShowToast");
            }
        },

        /**
         * Hàm click Không đồng ý cập nhật thông tin NVL, tắt dialog và tắt form cập nhật
         * params: ()
         * Author: CVViệt ()
         * Date: 01/06/2023
         */
        btnCancelUpdate() {
            this.gdialog.isShowDialog = false;
            this.$emit("closeForm");
            this.$emit("reload");
        },

        /**
         * Hàm click hủy bỏ dialog cập nhật thông tin NVL
         * params: ()
         * Author: CVViệt ()
         * Date: 01/06/2023
         */
        btnDisable() {
            this.material.stopFollowing = this.material.stopFollowing == 1 ? true : false;
            this.gdialog.isShowDialog = false;
        },

        showExpiryDropList() {
            this.isShowExpiryDropList = !this.isShowExpiryDropList;
        },
        chooseExpiry(expiry) {
            this.material.expiryText = expiry;
            this.isShowExpiryDropList = false;
        },

        /**
         * Hàm thêm 1 dòng đơn vị chuyển đổi
         * params: ()
         * Author: CVViệt ()
         * Date: 06/06/2023
         */
        addNewRowConvertion() {
            this.convertions += 1;
            this.unitConvertions[this.convertions - 1] = {};
            this.unitConvertions[this.convertions - 1].scale = 1;
            this.unitConvertions[this.convertions - 1].calculate = "*";
        },

        /**
         * Hàm xóa 1 dòng đơn vị chuyển đổi
         * params: ()
         * Author: CVViệt ()
         * Date: 06/06/2023
         */
        deleteRowConvertion(e) {
            if (this.convertions == 0) {
                e.preventDefault();
            }
            else {
                this.convertions -= 1;
                let deleteItem = this.unitConvertions.pop();
                this.arrayDeleteConvertion.push(deleteItem);
            }
        },


        /**
         * Hàm chọn ĐVT cho đơn vị chuyển đổi
         * params: ()
         * Author: CVViệt ()
         * Date: 06/06/2023
         */
        chooseConvertionUnit(result, i) {
            if (this.material.calculationUnitName != null && this.material.calculationUnitName != "") {
                if (result.value == this.material.calculationUnitName) {
                    this.gdialog.typeDialog = 'warning2';
                    this.gdialog.isShowDialog = true;
                    this.unitConvertions[i - 1].calculationUnitId = null;
                    this.unitConvertions[i - 1].calculationUnitName = null;
                }
                else {
                    let checked = false;
                    for (let z = 0; z < this.convertions; z++) {
                        if (z != i - 1 && this.unitConvertions[z].calculationUnitName && result.value == this.unitConvertions[z].calculationUnitName) {
                            checked = true;
                            break;
                        }
                    }
                    if (checked) {
                        this.gdialog.typeDialog = 'warning3';
                        this.gdialog.isShowDialog = true;
                        this.unitConvertions[i - 1].calculationUnitId = null;
                        this.unitConvertions[i - 1].calculationUnitName = null;
                    }
                    else {
                        this.unitConvertions[i - 1].calculationUnitId = result.key;
                        this.unitConvertions[i - 1].calculationUnitName = result.value;
                        this.autoGenerateDescription(i - 1);
                    }
                }
            }
            else {
                let checked = false;
                for (let z = 0; z < this.convertions; z++) {
                    if (z != i - 1 && this.unitConvertions[z].calculationUnitName && result.value == this.unitConvertions[z].calculationUnitName) {
                        checked = true;
                        break;
                    }
                }
                if (checked) {
                    this.gdialog.typeDialog = 'warning3';
                    this.gdialog.isShowDialog = true;
                }
                else {
                    this.unitConvertions[i - 1].calculationUnitId = result.key;
                    this.unitConvertions[i - 1].calculationUnitName = result.value;
                }
            }

        },

        /**
         * Hàm tự sinh mô tả cho đơn vị chuyển đổi
         * params: ()
         * Author: CVViệt ()
         * Date: 06/06/2023
         */
        autoGenerateDescription(i) {
            if (this.material && this.unitConvertions[i]) {
                if (this.material.calculationUnitName && this.unitConvertions[i].calculationUnitName) {
                    if (this.unitConvertions[i].calculate == "*") {
                        this.unitConvertions[i].description = "1 " + this.unitConvertions[i].calculationUnitName + " = " + this.unitConvertions[i].scale + " "
                            + this.unitConvertions[i].calculate + " " + this.material.calculationUnitName;
                    }
                    else {
                        this.unitConvertions[i].description = "1 " + this.unitConvertions[i].calculationUnitName + " = " + "1 " + this.unitConvertions[i].calculate + this.unitConvertions[i].scale + " "
                            + " " + this.material.calculationUnitName;
                    }
                }
            }
        },
    },
    emits: ["closeForm", "openNewForm", "reload", "onShowToast"]
}
</script>
<style scoped>
#form-container {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #0973b9;
    width: 750px;
    height: auto;
    border-radius: 6px;
    padding: 6px;
}

#form-header {
    height: 30px;
    padding: 0 4px;
    border-color: #0973b9;
    color: #fff;
    background-color: #0973b9;
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
    height: 36px;
    background-color: #fff;
    padding: 8px 0;
}

.textfield {
    width: 50%;
    display: flex;
    align-items: center;
}

.textfield-name {
    width: 120px;
    padding-left: 8px;
}

.textfield .textfield-input {
    width: 100%;
    max-width: 205px;
    height: 24px;
    padding: 0 4px;
    border: 1px solid #bbb;
}

.x-textfield-input {
    width: 100%;
    max-width: calc(100% - 156px);
    height: 48px;
    padding: 0 4px;
    border: 1px solid #bbb;
}

.textfield .textfield-input:focus,
.x-textfield-input:focus {
    outline: unset;
    border: 1px solid #0071c1 !important;
}

.operations {
    background-color: #f2f2f2;
    border: 1px solid #bbb;
    border-bottom: unset !important;
    padding: 2px;
}

.x-form-btn {
    height: 26px;
    padding: 4px;
    border-radius: 2px;
    border: 1px solid #f2f2f2;
    background: #f2f2f2;
    cursor: pointer;
}

.x-form-btn:hover {
    background-color: #b1ddfa;
}

.x-form-btn.x-form-btn--active {
    background-color: #fff;
    color: #0072bc;
    border: 1px solid #fcfcfc;
    border-top: 2px solid #0072bc !important;
    box-shadow: 1px 1px 2px #bbb;
}


.table-container {
    height: 180px;
    border: 1px solid #bbb;
    position: relative;
    overflow: auto;
}

.m-table-convertion {
    border-collapse: collapse;
}

.m-table-convertion thead {
    /* font-weight: 700; */
    position: sticky;
    top: 0;
    z-index: 60;
}

.m-table-convertion thead td {
    background-color: #f2f2f2;
    /* font-weight: 700; */
    position: sticky;
    top: 0;
    border-right: 1px solid #bbb;
    border-bottom: 1px solid #bbb;
    z-index: 60;
}

.m-table-convertion td .convertion-input {
    width: 123px;
    border: 1px solid #bbb;
    padding: 0 4px;
}

.m-table-convertion td .convertion-input:focus {
    outline: unset;
    border: 1px solid #0071c1 !important;
}


.row-convertion {
    width: 100%;
    height: 30px;
    background-color: #f2f2f2;
}

.row-convertion span {
    padding: 6px;
    flex-grow: 2;
    display: flex;
    align-items: center;
    justify-content: center;
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

.icon-add {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -2648px;
    width: 14px;
    height: 16px;
    margin-right: 4px;
}

.icon-delete {
    background: url('../../assets/img/IconSprite.png') no-repeat -2px -2683px;
    width: 12px;
    height: 11px;
    margin-right: 4px;
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

.icon-save-renew {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -737px;
    width: 16px;
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