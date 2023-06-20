import BaseService from "./base/BaseService";
import baseAPI from "@/resources/baseAPI";
import axios from "axios";

export default class WarehouseService extends BaseService {

    constructor(controller = "Warehouse") {
        super(controller);
    }

    getPaging(pageSize = 25, pageNumber = 1, stringCode, stringName, stringAddress) {
        try {
            let warehouseRequestURL = baseAPI + this.controller + "/Pagination" + "?pageSize=" + pageSize + "&pageNumber=" + pageNumber;
            if (stringCode != null) {
                warehouseRequestURL += "&stringCode=" + stringCode;
            }
            if (stringName != null) {
                warehouseRequestURL += "&stringName=" + stringName;
            }
            if (stringAddress != null) {
                warehouseRequestURL += "&stringAddress=" + stringAddress;
            }
            return axios.get(warehouseRequestURL);
        } catch (error) {
            console.log(error);
        }
    }

}