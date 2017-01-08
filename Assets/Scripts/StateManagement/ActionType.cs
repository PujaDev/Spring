﻿using UnityEngine;
using System.Collections;

public enum ActionType {
    TELL_FORTUNE = 1,
    PUT_ON,
    SELL,
    TURN_ALARM_OFF,
    POSTPONE_ALARM,
    TURN_MUSIC_ON,
    TURN_MUSIC_OFF,
    CHANGE_CLOTHES,
    CALL_MOM,
    LOOK,
    TAKE,
    START_READING_VEGAN_BOOK,
    STOP_READING_VEGAN_BOOK,
    GO_OUTSIDE,
    GO_INSIDE,
    FLY_AWAY,
    EMPTY_BOILER,
    THROW_TO_BOILER,
    FILL_ELIXIR,
    GIVE_ADDRESS_TO_OWL,
    GIVE_PACKAGE_TO_OWL,
    START_READING_FRIDGE_NOTE,
    STOP_READING_FRIDGE_NOTE,


    // Forest actions
    GIVE_MONEY_TO_SHRINE,
    BUY_IN_FOREST,
    START_READING_MAP,
    STOP_READING_MAP,
    GO_TO_FOREST,
    GO_FOREST_LEFT,
    GO_FOREST_RIGHT,
    GO_FOREST_FINISH,

    // Huba TortoiseBus actions
    GET_TICKET,
    GET_ELIXIR,
    DELIVERY,

    // Added to Annana House - if I put it at the beggining it breaks several references in first scene
    RESET_ALARM,


    // Annana Tea Party actions
    FILL_TEAPOT,
    PUT_TEAPON_ON_THE_STOVE
}
