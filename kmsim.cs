<style>
tr > th {
    padding-left: 10px;
}

/* #approve_button {
    padding: 10px 30px;
} */

#approve_button > button {
    margin-right: 15px;
}
</style>
<div style="position: absolute; right: 0; top: 20px;">
    <button ng-if="($$.lm.status == 1 || $$.lm.status == 3) && $$.selectedNode.type !='table_other' && $$.influnenceUrl.length == 0" id="editLmButton" class="btn dg-btn" style="margin-right: 10px;" ng-click="editLm()">
        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> {{'DG.EDIT' | translate}}
    </button>
    <button ng-if="$$.lm.status == 1 && $$.selectedNode.type !='table_other'" class="btn dg-btn dg-success" id="submitLmButton" style="margin-right: 10px;" ng-click="submitLm()">
        <span class="glyphicon glyphicon-chevron-up" aria-hidden="true"></span> {{'DG.MODEL.SUBMIT' | translate}}
    </button>
    <button ng-if="$$.lm.status == 3 && $$.selectedNode.type !='table_other' && $Model.proEnv!=''" class="btn dg-btn dg-success" style="margin-right: 10px;" id="onlineModelButton" ng-click="onlineModel()">
        <span class="glyphicon glyphicon-chevron-up" aria-hidden="true"></span> {{'DG.ONLINE' | translate}}
    </button>
    <button ng-if="$$.lm.status == 1 && $$.selectedNode.type !='table_other'" class="btn dg-btn dg-alert" id="deleteLmButton" style="margin-right: 10px;" ng-click="deleteLm()">
        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> {{'DG.DELETE' | translate}}
    </button>
    <button ng-if="(($$.envType == '0' && $$.lm.status == 9) || ($$.envType == '-1' && $$.lm.status == 2)) && $$.currentpage == 'main.model.lm' && $$.selectedNode.type !='table_other'" id="revokeLmButton" class="btn dg-btn dg-alert" style="margin-right: 10px;" ng-click="revokeLm()">
        <span class="glyphicon glyphicon-chevron-down" aria-hidden="true"></span> {{'DG.MODEL.REVOKE' | translate}}
    </button>
    <button ng-if="$$.lm.status == 3 && $$.selectedNode.type !='table_other' && $$.influnenceUrl.length == 0" class="btn dg-btn dg-alert" style="margin-right: 10px;" id="revokeApprovedModelButton" ng-click="revokeApprovedModel()">
        <span class="glyphicon glyphicon-chevron-down" aria-hidden="true"></span> {{'DG.MODEL.REVOKE' | translate}}
    </button>
</div>
<div style="position: absolute; right: 0; top: 50px;">
<tabset justified="true" class="dg-tab" style="padding-top: 5px;">
    <tab heading="{{'DG.MODEL.DATA_MODEL_AND_STRUCTURE' | translate}}">
        <div style="padding-left: 10px;">
            <fieldset class="dg-fieldset">
                <legend class="dg-legend">{{'DG.MODEL.MODEL_INFO' | translate}}</legend>
                <table class="table">
                    <tr>
                        <th>{{'DG.BUSINESS_NAME' | translate}}</th>
                        <td>{{$$.lm.businessName}}</td>
                        <th>{{'DG.ENTITY_NAME' | translate}}</th>
                        <td>{{$$.lm.entityName}}</td>
                    </tr>
                    <tr>
                        <th>{{'DG.MODEL.ENTITY_TYPE' | translate}}</th>
                        <td>{{$$.lm.tableTypeName}}</td>
                        <th>{{'DG.DESCRIPTION' | translate}}</th>
                        <td>
                            <textarea id="lMDescription" readonly="readonly" style="border:none; width:100%; height:100%;">{{$$.lm.description}}</textarea>
                        </td>
                    </tr>
                    <tr ng-repeat="p in $Model.lmExtendProp track by p.propertyId" ng-if="$even" ng-init="current = $Model.lmExtendProp[$index]; next = $Model.lmExtendProp[$index + 1]; currentValue = $$.lm[current.columnName]; nextValue = $$.lm[next.columnName];">
                        <th>{{current.propertyName}}</th>
                        <td ng-if="next">
                            <div ng-switch on="current.editType">
                                <span ng-switch-when="5">{{currentValue | date: 'yyyy-MM-dd'}}</span>
                                <span ng-switch-when="6">{{currentValue | date: 'HH:mm:ss'}}</span>
                                <span ng-switch-when="7">{{currentValue | date: 'yyyy-MM-dd HH:mm:ss'}}</span>
                                <span ng-switch-default>{{currentValue}}</span>
                            </div>
                        </td>
                        <td ng-if="!next" colspan="3">
                            <div ng-switch on="current.editType">
                                <span ng-switch-when="5">{{currentValue | date: 'yyyy-MM-dd'}}</span>
                                <span ng-switch-when="6">{{currentValue | date: 'HH:mm:ss'}}</span>
                                <span ng-switch-when="7">{{currentValue | date: 'yyyy-MM-dd HH:mm:ss'}}</span>
                                <span ng-switch-default>{{currentValue}}</span>
                            </div>
                        </td>
                        <th ng-if="next">{{next.propertyName}}</th>
                        <td ng-if="next">
                            <div ng-switch on="next.editType">
                                <span ng-switch-when="5">{{nextValue | date: 'yyyy-MM-dd'}}</span>
                                <span ng-switch-when="6">{{nextValue | date: 'HH:mm:ss'}}</span>
                                <span ng-switch-when="7">{{nextValue | date: 'yyyy-MM-dd HH:mm:ss'}}</span>
                                <span ng-switch-default>{{nextValue}}</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>{{'DG.CREATE_OPERATOR' | translate}}</th>
                        <td>{{$$.lm.createOperator}}</td>
                        <th>{{'DG.CREATE_TIME' | translate}}</th>
                        <td>{{$$.lm.createTime}}</td>
                    </tr>
                    <tr>
                        <th>{{'DG.LAST_MODIFY_OPERATOR' | translate}}</th>
                        <td>{{$$.lm.lastModifyOperator}}</td>
                        <th>{{'DG.LAST_MODIFY_TIME' | translate}}</th>
                        <td>{{$$.lm.lastModifyTime}}</td>
                    </tr>
                </table>
            </fieldset>
            <fieldset class="dg-fieldset">
                <legend class="dg-legend">{{'DG.MODEL.DATA_STRUCTURE' | translate}}</legend>
                <table class="table table-striped dg-table">
                    <thead>
                        <tr>
                            <th width="15%">{{'DG.BUSINESS_NAME' | translate}}</th>
                            <th width="15%">{{'DG.ATTRIBUTE_NAME' | translate}}</th>
                            <th width="10%" ng-if="$$.tableType == 'TT_DIM_TYPE2'">{{'DG.COLUMN.DIM_KEY' | translate}}</th>
                            <th width="10%">{{'DG.COLUMN.DATA_TYPE' | translate}}</th>
                            <th width="10%">{{'DG.COLUMN.DATA_LENGTH' | translate}}</th>
                            <th width="10%">{{'DG.COLUMN.DATA_PRECISION' | translate}}</th>
                            <th width="10%">{{'DG.COLUMN.DATA_FORMAT' | translate}}</th>
                            <th width="10%">{{'DG.COLUMN.NULL_ABLE' | translate}}</td>
                                <th>{{'DG.DESCRIPTION' | translate}}</th>
                                <th ng-repeat="prop in $Model.columnExtendProp track by prop.propertyId">
                                    <span ng-if="prop.isDisplayList == 1">{{prop.propertyName}}</span>
                                </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr ng-repeat="col in $$.columns track by col.columnId">
                            <td>{{col.businessName}}</td>
                            <td>{{col.attributeName}}</td>
                            <td ng-if="$$.tableType == 'TT_DIM_TYPE2'">{{$$.isDimKeyConfig[col.isDimKey]}}</td>
                            <td>{{col.dataType | config: $Model.dataTypes}}</td>
                            <td>{{col.dataLength}}</td>
                            <td>{{col.dataPrecision}}</td>
                            <td ng-if="col.format">{{col.format | config: $Model.dataFormats}}</td>
                            <td ng-if="!col.format">--.--</td>
                            <td ng-if="col.notNull == 0">
                                <span class="glyphicon glyphicon-stop" aria-hidden="true" style="padding-right:10px;color:LimeGreen;"></span> {{'DG.YES' | translate}}
                            </td>
                            <td ng-if="col.notNull != 0">
                                <span class="glyphicon glyphicon-stop" aria-hidden="true" style="padding-right:10px;color:red;"></span> {{'DG.NO' | translate}}
                            </td>
                            <td>{{col.description}}</td>
                            <td ng-repeat="prop in $Model.columnExtendProp track by prop.propertyId">
                                <div ng-if="prop.isDisplayList == 1" ng-switch on="prop.editType">
                                    <span ng-switch-when="5">{{col[prop.columnName] | date: 'yyyy-MM-dd'}}</span>
                                    <span ng-switch-when="6">{{col[prop.columnName] | date: 'HH:mm:ss'}}</span>
                                    <span ng-switch-when="7">{{col[prop.columnName] | date: 'yyyy-MM-dd HH:mm:ss'}}</span>
                                    <input id="{{prop.propertyId}}" ng-switch-when="8" type="password" value="******" readonly style="border:none;" />
                                    <span ng-switch-default>{{col[prop.columnName]}}</span>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div>
                    <dg-pagination conf="paginationConf"></dg-pagination>
                </div>
            </fieldset>
        </div>
    </tab>
    <tab heading="{{'DG.MODEL.STORAGE_RULE' | translate}}" ng-if="$$.dataCycles.length != 0" select="HtRender()">
        <div style="padding-left: 10px;">
            <fieldset class="dg-fieldset">
                <ng-include src="'partials/model/lm/dataLifeCycleDetailBody.html'"></ng-include>
            </fieldset>
        </div>
    </tab>
    <tab heading="{{'DG.MODEL.INFLUENCE_ANALYSE' | translate}}" ng-if="$$.influnenceUrl.length != 0">
        <div style="padding-left: 10px;">
            <fieldset class="dg-fieldset">
                <iframe name="influenceframe" id="influenceframe" src="{{$$.influnenceUrl}}" style="width:100%;height:600px; min-width: 0;padding: 0; margin: 0;border: 0;"></iframe>
            </fieldset>
        </div>
    </tab>
    <tab heading="{{'DG.MODEL.CONSANGUINITY_ANALYSE' | translate}}" ng-if="$$.consanguinityUrl.length != 0">
        <div style="padding-left: 10px;">
            <fieldset class="dg-fieldset">
                <iframe name="consanguinityframe" id="consanguinityframe" src="{{$$.consanguinityUrl}}" style="width:100%;height:600px; min-width: 0;padding: 0; margin: 0;border: 0;"></iframe>
            </fieldset>
        </div>
    </tab>
    <tab heading="{{'DG.MODEL.WHOLECHAIN_ANALYSE' | translate}}" ng-if="$$.wholeChainUrl.length != 0">
        <div style="padding-left: 10px;">
            <fieldset class="dg-fieldset">
                <iframe name="wholeChainframe" id="wholeChainframe" src="{{$$.wholeChainUrl}}" style="width:100%;height:600px; min-width: 0;padding: 0; margin: 0;border: 0;"></iframe>
            </fieldset>
        </div>
    </tab>
</tabset>
</div>
<div class="col-lg-12" style="margin-top:5px" ng-if="$$.operate=='approve'">
    <div id="approve_button" style="text-align:right;" ng-if="$$.lm.status == '2'">
        <button id="refuseButton" class="btn dg-btn large hollow dg-center" ng-click="approveRefuseOnline($$.lm.lmId,$$.lm.versionNo)">
            {{'DG.REFUSE' | translate}}
        </button>
        <button id="passButton" class="btn dg-btn large dg-center" ng-click="approvePass($$.lm.lmId,$$.lm.versionNo)">
            {{'DG.PASS' | translate}}
        </button>
    </div>
    <div id="approve_button" style="text-align:right;" ng-if="$$.lm.status == '9'">
        <button id="refuseButton" class="btn dg-btn large hollow dg-center" ng-click="approveRefuseDownLine($$.lm.lmId,$$.lm.versionNo)">
            {{'DG.REFUSE' | translate}}
        </button>
        <button id="downlineButton" class="btn dg-btn large dg-center" ng-click="downLine($$.lm.lmId,$$.lm.versionNo)">
            {{'DG.MODEL.DOWN_LINE' | translate}}
        </button>
    </div>
</div>
