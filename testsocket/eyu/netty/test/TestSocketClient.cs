/**
using UnityEngine;
using System.Collections;
using org.eyu.netty.socket;
using org.eyu.netty.util;

public class TestSocketClient : MonoBehaviour {
	private static ILogger logger = LoggerManager.buildLogger("TestSocketClient");
	private static SocketClient socketClient;
 	void Awake  () {		
		logger.debug("start");
		 socketClient =new SocketClient(SocketConfig.valueOf("127.0.0.1",8889));
		 socketClient.start();
	}
	void Update(){
		if(socketClient!=null){
			socketClient.startReceive();
		}
	}
	void OnApplicationQuit (){
		logger.debug("OnApplicationQuit");
		socketClient.close();
	}
 
}*/
