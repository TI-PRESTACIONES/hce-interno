/// <reference path="ADD_FACTOR.js" />


    var COLOR_BLUE01 = "#284A89";
    var COLOR_RED01 = "#990000";

    var changeEstado = function (value) {
        var template = '<span style="color:{0};">{1}</span>';
        if (value == 2)
            return Ext.String.format(template, COLOR_BLUE01, "ACTIVO");
        if (value == 1)
            return Ext.String.format(template, COLOR_RED01, "INACTIVO");
    };


    var eventoCargarMain2 = function () {

        Ext.net.DirectMethod.request({
            url: 'addPermisosFormatos',
            params: {
                data: 'FORMATO',
                indica: 'PERMISO',
            },
            success: function (result) { accionSetPermisos(result.data); }
        });
    };
    var accionSetPermisos = function (data) {
        var mensajes = "";
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].ACCION == 'BUSCAR' && App.compoBUSCAR != null) {
                    App.compoBUSCAR.setVisible((data[i].campoInt01 == 2 ? true : false));
                } else if (data[i].ACCION == 'NUEVO' && App.compoNUEVO != null) {
                    App.compoNUEVO.setVisible((data[i].campoInt01 == 2 ? true : false));
                } else if (data[i].ACCION == 'MODIFICAR' && App.compoMODIFICAR != null) {
                    App.compoMODIFICAR.setVisible((data[i].campoInt01 == 2 ? true : false));
                } else if (data[i].ACCION == 'ELIMINAR' && App.compoELIMINAR != null) {
                    App.compoELIMINAR.setVisible((data[i].campoInt01 == 2 ? true : false));
                } else if (data[i].ACCION == 'VER' && App.compoVER != null) {

                } else if (data[i].ACCION == 'VER2' && App.compoVER != null) {
                    App.compoVER.setVisible((data[i].campoInt01 == 2 ? true : false));
                } else if (data[i].ACCION == 'ACCESO' && App.compoACCESO != null) {
                    App.compoACCESO.setVisible((data[i].campoInt01 == 2 ? true : false));
                } else if (data[i].ACCION == 'HABILITADO' && App.compoHABILITADO != null) {
                    App.compoHABILITADO.setVisible((data[i].campoInt01 == 2 ? true : false));
                } else if (data[i].ACCION == 'OBLIGA' && App.compoOBLIGA != null) {
                    App.compoOBLIGA.setVisible((data[i].campoInt01 == 2 ? true : false));
                } else if (data[i].ACCION == 'PRIORIDADAGOPCION' && App.compoPRIORIDADAGOPCION != null) {
                    App.compoPRIORIDADAGOPCION.setVisible((data[i].campoInt01 == 2 ? true : false));
                }

            }
        }
    };


    var seleccionActual = null;
    function setSeleccion(param1) {
        seleccionActual = param1;
    };

    var eventoCheckFiltro = function (check, componente) {
        var checkAll = check.getValue();
        if (checkAll) {
            componente.setDisabled(true);
            componente.setValue(null);
        } else {
            componente.setDisabled(false);
        }
    };


    var onSuccess = function (grid, data) {
        grid.show();
        grid.getStore().loadData(data);
    };



    var varTipoBuscar = "LOAD";
    var accionBuscarFiltro = function (tipo) {
        varTipoBuscar = tipo;
        App.GridPanelVerFactor.getStore().reload();
        varTipoBuscar = 'LOAD';
    };
    var varTipoBuscar2 = "LOAD";
    var accionBuscarFiltro2 = function (tipo) {
        varTipoBuscar2 = tipo;
        App.GridPanelVerIntervenciones.getStore().reload();
        varTipoBuscar2 = 'LOAD';
    };
    var varTipoBuscar3 = "LOAD";
    var accionBuscarFiltro3 = function (tipo) {
        varTipoBuscar3 = tipo;
        App.GridPanelVerActividades.getStore().reload();
        varTipoBuscar3 = 'LOAD';
    };
    var varTipoBuscarX = "LOAD";
    var accionBuscarFiltroX = function (tipo) {
        varTipoBuscarX = tipo;
        App.GridPanelVerIndicador.getStore().reload();
        varTipoBuscarX = 'LOAD';
    };
    var varTipoBuscarObj = "LOAD";
    var accionBuscarFiltroObj = function (tipo) {
        varTipoBuscarObj = tipo;
        App.GridPanelVerObjetivo.getStore().reload();
        varTipoBuscarObj = 'LOAD';
    };
    /////////////

    var eventoAgregarFormulario = function (window, nanda) {

        if (window != null) {

            var records = App.GridPanelVerFactor.getSelectionModel().getSelection();
            var ArrayList0 = new Array();

            for (var i = 0; i < records.length; i++) {
                var MA_MiscelaneosDetalle = {
                    ValorCodigo2: records[i].get('IdFactorRelacionado'),
                    CodigoElemento: records[i].get('Codigo'),
                    DescripcionLocal: records[i].get('Descripcion'),
                    ValorCodigo5: records[i].get('Estado'),
                    ValorCodigo1: records[i].get('Orden'),
                    ValorEntero1: parseInt(nanda)
                    //ValorCodigo2: records[i].get('nivel')



                };
                ArrayList0.push(MA_MiscelaneosDetalle);

            }

            Ext.net.DirectMethod.request({
                url: 'eventoAgregarHistoricoFormulario2',
                params: {

                    idWindow: 'WindowFactor',
                    accion: "AGREGAR",
                    data: ArrayList0
                }
                , success: function (result) {

                    if (result.data != null) {

                        parent.App.PanelA.getBody().App.RecepcionaRecurso2.setValue(JSON.stringify(MA_MiscelaneosDetalle));
                    }
                }
            });
        } else {

        }
    };



    var eventoAgregarFormulario2 = function (window, nanda) {

        if (window != null) {

            var records = App.GridPanelVerObjetivo.getSelectionModel().getSelection();
            var ArrayList0 = new Array();

            for (var i = 0; i < records.length; i++) {
                var MA_MiscelaneosDetalle = {
                    ValorCodigo2: records[i].get('IdNanNoc'),
                    CodigoElemento: records[i].get('IdNoc'),
                    DescripcionLocal: records[i].get('Descripcion'),
                    ValorCodigo5: records[i].get('Estado'),
                    ValorCodigo1: records[i].get('IdNanda'),
                    ValorEntero1: parseInt(nanda)
                    //ValorCodigo2: records[i].get('nivel')



                };
                ArrayList0.push(MA_MiscelaneosDetalle);
                
            }

            Ext.net.DirectMethod.request({
                url: 'eventoAgregarHistoricoFormulario3',
                params: {

                    idWindow: 'WindowObjetivo',
                    accion: "AGREGAR",
                    data: ArrayList0
                }
                , success: function (result) {

                    if (result.data != null) {

                        parent.App.PanelA.getBody().App.RecepcionaRecurso3.setValue(JSON.stringify(MA_MiscelaneosDetalle));
                    }
                }
            });
        } else {

        }
    };

    var eventoAgregarFormulario3 = function (window, nanda, noc) {

        if (window != null) {

            var records = App.GridPanelVerIntervenciones.getSelectionModel().getSelection();
            var ArrayList0 = new Array();

            for (var i = 0; i < records.length; i++) {
                var MA_MiscelaneosDetalle = {
                    ValorCodigo2: records[i].get('IdNC'),
                    CodigoElemento: records[i].get('IdNoc'),
                    DescripcionLocal: records[i].get('Descripcion'),
                    ValorCodigo5: records[i].get('Estado'),
                    ValorCodigo1: records[i].get('IdNic'),
                    ValorEntero1: parseInt(nanda),
                    ValorEntero2: parseInt(noc)
                    //ValorCodigo2: records[i].get('nivel')



                };
                ArrayList0.push(MA_MiscelaneosDetalle);

            }

            Ext.net.DirectMethod.request({
                url: 'eventoAgregarHistoricoFormulario4',
                params: {

                    idWindow: 'WindowIntervencion',
                    accion: "AGREGAR",
                    data: ArrayList0
                }
                , success: function (result) {

                    if (result.data != null) {

                        parent.App.PanelA.getBody().App.RecepcionaRecurso4.setValue(JSON.stringify(MA_MiscelaneosDetalle));
                    }
                }
            });
        } else {

        }
    };

    var eventoAgregarFormulario4 = function (window, nanda, noc, nic) {

        if (window != null) {

            var records = App.GridPanelVerActividades.getSelectionModel().getSelection();
            var ArrayList0 = new Array();

            for (var i = 0; i < records.length; i++) {
                var MA_MiscelaneosDetalle = {
                    ValorCodigo2: records[i].get('IdNA'),
                    CodigoElemento: records[i].get('IdNic'),
                    DescripcionLocal: records[i].get('Descripcion'),
                    ValorCodigo5: records[i].get('Estado'),
                    ValorCodigo1: records[i].get('IdActividad'),
                    ValorEntero1: parseInt(nanda),
                    ValorEntero2: parseInt(noc),
                    ValorEntero3: parseInt(nic)
                    //ValorCodigo2: records[i].get('nivel')



                };
                ArrayList0.push(MA_MiscelaneosDetalle);

            }

            Ext.net.DirectMethod.request({
                url: 'eventoAgregarHistoricoFormulario5',
                params: {

                    idWindow: 'WindowActividadPAE',
                    accion: "AGREGAR",
                    data: ArrayList0
                }
                , success: function (result) {

                    if (result.data != null) {

                        parent.App.PanelA.getBody().App.RecepcionaRecurso5.setValue(JSON.stringify(MA_MiscelaneosDetalle));
                    }
                }
            });
        } else {

        }
    };

    var eventoAgregarFormularioX = function (window, nanda, noc) {

        if (window != null) {

            var records = App.GridPanelVerIndicador.getSelectionModel().getSelection();
            var ArrayList0 = new Array();

            for (var i = 0; i < records.length; i++) {
                var MA_MiscelaneosDetalle = {
                    ValorCodigo2: records[i].get('IdNIN'),
                    CodigoElemento: records[i].get('IdIndicadorPAE'),
                    DescripcionLocal: records[i].get('Descripcion'),
                    ValorCodigo5: records[i].get('Estado'),
                    ValorCodigo1: parseInt(noc),/*records[i].get('IdNoc'),*/
                    ValorEntero1: parseInt(nanda)
                    //ValorCodigo2: records[i].get('nivel')



                };
                ArrayList0.push(MA_MiscelaneosDetalle);

            }

            Ext.net.DirectMethod.request({
                url: 'eventoAgregarHistoricoFormularioXY',
                params: {

                    idWindow: 'WindowIndicador',
                    accion: "AGREGAR",
                    data: ArrayList0
                }
                , success: function (result) {

                    if (result.data != null) {

                        parent.App.PanelA.getBody().App.RecepcionaRecursoX.setValue(JSON.stringify(MA_MiscelaneosDetalle));
                    }
                }
            });
        } else {

        }
    };














