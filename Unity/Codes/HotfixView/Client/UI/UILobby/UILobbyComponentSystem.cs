﻿using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendClass(typeof(UILobbyComponent))]
    public static class UILobbyComponentSystem
    {
        [ObjectSystem]
        public class UILobbyComponentAwakeSystem: AwakeSystem<UILobbyComponent>
        {
            public override void Awake(UILobbyComponent self)
            {
                ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

                self.enterMap = rc.Get<GameObject>("EnterMap");
                self.enterMap.GetComponent<Button>().onClick.AddListener(() => { self.EnterMap().Coroutine(); });
            }
        }
        
        public static async ETTask EnterMap(this UILobbyComponent self)
        {
            await EnterMapHelper.EnterMapAsync(self.ClientScene());
            await UIHelper.Remove(self.ClientScene(), UIType.UILobby);
        }
    }
}