using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PalletMover : ManejoPallets {


    public MoveType miInput;
    public enum MoveType {
        WASD,
        Arrows
    }
    
    public Slider slider;

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private void Update() {
        slider.gameObject.SetActive(true);


        switch(miInput){
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && slider.value > 0f&& slider.value<0.45f||Input.GetKeyDown("a")) {
                    PrimerPaso();
                }
                if (Tenencia() && slider.value > 0.45f&&slider.value<1f||Input.GetKeyDown("w")) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && slider.value==1||Input.GetKeyDown("d")) {
                    TercerPaso();
                }
                break;
            case MoveType.Arrows:
            if (!Tenencia() && Desde.Tenencia() && slider.value > 0f&& slider.value<0.45f||Input.GetKeyDown("left")) {
                    PrimerPaso();
                }
                if (Tenencia() && slider.value > 0.45f&&slider.value<1f||Input.GetKeyDown("up")) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && slider.value==1||Input.GetKeyDown("right")) {
                    TercerPaso();
                }
                break;
        }


    }

    /*public void hacerTodoXD(){
        PrimerPaso();
        SegundoPaso();
        TercerPaso();
    }*/

    void PrimerPaso() {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso() {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    public void TercerPaso() {
        Dar(Hasta);
        segundoCompleto = false;
        slider.gameObject.SetActive(false);
    }

    public override void Dar(ManejoPallets receptor) {
        if (Tenencia()) {
            if (receptor.Recibir(Pallets[0])) {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet) {
        if (!Tenencia()) {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}
