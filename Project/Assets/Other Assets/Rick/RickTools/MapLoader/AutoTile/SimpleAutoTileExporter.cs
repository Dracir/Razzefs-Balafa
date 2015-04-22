#if UNITY_EDITOR 
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using Rick.TiledMapLoader;

namespace RickTools.MapLoader{
	
	public class SimpleAutoTileExporter {
	
		XmlDocument doc;
		XmlNode map;
		
		string sourceTileset;
		ulong center;
		ulong sideNorth;
		ulong sideSouth;
		ulong sideWest;
		ulong sideEst;
		ulong cornerInNW;
		ulong cornerInNE;
		ulong cornerInSW;
		ulong cornerInSE;
		ulong cornerOutNW;
		ulong cornerOutNE;
		ulong cornerOutSW;
		ulong cornerOutSE;
		
		public SimpleAutoTileExporter(string sourceTileset, int idCenter, int idSide, int idCornerIn, int idCornerOut) {
			this.sourceTileset = sourceTileset;
			this.center = (ulong)(idCenter + 1);
			
			this.sideNorth = (ulong)(idSide + 1);
			this.sideEst = (ulong)(idSide + 1) + TiledMapLoader.ROTATION_90_FLAG;
			this.sideSouth = (ulong)(idSide + 1) + TiledMapLoader.ROTATION_180_FLAG;
			this.sideWest = (ulong)(idSide + 1) + TiledMapLoader.ROTATION_270_FLAG;
			
			this.cornerInNW = (ulong)(idCornerIn + 1);
			this.cornerInNE = (ulong)(idCornerIn + 1) + TiledMapLoader.ROTATION_90_FLAG;
			this.cornerInSE = (ulong)(idCornerIn + 1) + TiledMapLoader.ROTATION_180_FLAG;
			this.cornerInSW = (ulong)(idCornerIn + 1) + TiledMapLoader.ROTATION_270_FLAG;
			
			this.cornerOutNW = (ulong)(idCornerOut + 1);
			this.cornerOutNE = (ulong)(idCornerOut + 1) + TiledMapLoader.ROTATION_90_FLAG;
			this.cornerOutSE = (ulong)(idCornerOut + 1) + TiledMapLoader.ROTATION_180_FLAG;
			this.cornerOutSW = (ulong)(idCornerOut + 1) + TiledMapLoader.ROTATION_270_FLAG;
		}
		
		public XmlDocument generateDocument(){
			doc = new XmlDocument();
			
			createAndAppendMapNode();
			createAndAppendAttributes();
			addTileset(sourceTileset);
			
			createAndAppendRegionsLayer();
			createAndAppendInputCenter();
			createAndAppendInputSides();
			createAndAppendInputCornerOut();
			createAndAppendInputCornerIn();
			createAndAppendOutputLayer();
			
			return doc;
		}
		
		
		

		void createAndAppendMapNode() {
			map = doc.CreateElement("map");
			doc.AppendChild(map);
			
			map.Attributes.Append(createAttribut(doc,"version","1.0"));
			map.Attributes.Append(createAttribut(doc,"orientation","orthogonal"));
			map.Attributes.Append(createAttribut(doc,"renderorder","right-up"));
			map.Attributes.Append(createAttribut(doc,"width","3"));
			map.Attributes.Append(createAttribut(doc,"height","43"));
			map.Attributes.Append(createAttribut(doc,"tilewidth","32"));
			map.Attributes.Append(createAttribut(doc,"tileheight","32"));
			map.Attributes.Append(createAttribut(doc,"nextobjectid","1"));
		}	

		void createAndAppendAttributes() {
			XmlNode properties = doc.CreateElement("properties");
			map.AppendChild(properties);
			
			XmlNode automappingRadius = doc.CreateElement("property");
			properties.AppendChild(automappingRadius);
			automappingRadius.Attributes.Append(createAttribut(doc,"name","AutomappingRadius"));
			automappingRadius.Attributes.Append(createAttribut(doc,"value","3"));
			
			XmlNode NoOverlappingRules = doc.CreateElement("property");
			properties.AppendChild(NoOverlappingRules);
			NoOverlappingRules.Attributes.Append(createAttribut(doc,"name","NoOverlappingRules"));
			NoOverlappingRules.Attributes.Append(createAttribut(doc,"value","True"));
			
		}	

		void addTileset(string sourceTileSet) {
			XmlNode tileset = doc.CreateElement("tileset");
			map.AppendChild(tileset);
			tileset.Attributes.Append(createAttribut(doc,"firstgid","1"));
			tileset.Attributes.Append(createAttribut(doc,"source",sourceTileSet));
		}

		
		XmlNode currentDataNode;
		void createAndAppendRegionsLayer() {
			currentDataNode = createAndAppendLayerReturnDataNode("regions", "3", "43", "0");
			
			//CornerOUT
			appendCurrentDataNodeLine(center,center, 0);
			appendCurrentDataNodeLine(center,center, center);
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,0, 0);
			
			appendCurrentDataNodeLine(0,center, center);
			appendCurrentDataNodeLine(center,center, center);
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,0, 0);
			
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(center,center, center);
			appendCurrentDataNodeLine(0,center, center);
			appendCurrentDataNodeLine(0,0, 0);
			
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(center,center, center);
			appendCurrentDataNodeLine(center,center, 0);
			appendCurrentDataNodeLine(0,0,0);
			
			//CornerIN
			appendCurrentDataNodeLine(center,center, 0);
			appendCurrentDataNodeLine(center,center, center);
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,0, 0);
			
			appendCurrentDataNodeLine(0,center, center);
			appendCurrentDataNodeLine(center,center, center);
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,0, 0);
			
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(center,center, center);
			appendCurrentDataNodeLine(0,center, center);
			appendCurrentDataNodeLine(0,0, 0);
			
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(center,center, center);
			appendCurrentDataNodeLine(center,center, 0);
			appendCurrentDataNodeLine(0,0,0);
			
			//SIDES
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,0, 0);
			
			appendCurrentDataNodeLine(center,center,center);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,center, 0);
			appendCurrentDataNodeLine(0,0, 0);
			
			appendCurrentDataNodeLine(center,center,center);
			
			fixCurrentDataEndLine();
		}

		void createAndAppendInputCenter() {
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "43", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//Side
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,center,0);
			
			fixCurrentDataEndLine();
			
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "43", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,center);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(center,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(center,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,center);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,center);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(center,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(center,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,center);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//SIDES
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(center,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,center);
			
			fixCurrentDataEndLine();
			
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "31", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,center,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			fixCurrentDataEndLine();
		}
		
		void createAndAppendInputSides() {
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "43", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,sideNorth);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(sideNorth,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(sideSouth,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,sideSouth);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(sideNorth,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,sideNorth);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,sideSouth);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(sideSouth,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//SIDE
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,sideSouth,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(sideWest,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,sideNorth,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,sideEst);
			
			fixCurrentDataEndLine();
			
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "31", "0");
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,sideWest,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,sideEst,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,sideEst,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,sideWest,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,sideWest,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,sideEst,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,sideEst,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,sideWest,0);
			
			fixCurrentDataEndLine();
		}

	
		void createAndAppendInputCornerOut() {
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "31", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,cornerOutNE);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(cornerOutNW,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(cornerOutSW,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,cornerOutSE);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(cornerOutNW,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,cornerOutNE);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,cornerOutSE);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(cornerOutSW,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			fixCurrentDataEndLine();
			
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "31", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerOutSW,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerOutSE,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,cornerOutNE,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,cornerOutNW,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,cornerOutNW,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,cornerOutNE,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerOutSE,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerOutSW,0);
			
			fixCurrentDataEndLine();
		}
		
		void createAndAppendInputCornerIn() {
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "31", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,cornerInNW);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(cornerInNE,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(cornerInSE,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,cornerInSW);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(cornerInNE,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,cornerInNW);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,cornerInSW);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(cornerInSE,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			fixCurrentDataEndLine();
			
			currentDataNode = createAndAppendLayerReturnDataNode("input_Tiles", "3", "31", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerInNW,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerInNE,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,cornerInSE,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,cornerInSW,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,cornerInSW,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,cornerInSE,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerInNE,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerInNW,0);
			
			fixCurrentDataEndLine();
		}
		
		#region output
		void createAndAppendOutputLayer() {
			currentDataNode = createAndAppendLayerReturnDataNode("output_Tiles", "3", "43", "0");
			
			//OUT
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerOutNW,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerOutNE,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerOutSE,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerOutSW,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//IN
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerInNW,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerInNE,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerInSE,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,cornerInSW,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			//SIDE
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,sideNorth,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,sideEst,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,sideSouth,0);
			appendCurrentDataNodeLine(0,0,0);
			appendCurrentDataNodeLine(0,0,0);
			
			appendCurrentDataNodeLine(0,sideWest,0);
			
			fixCurrentDataEndLine();
		}
		#endregion
		
		void appendCurrentDataNodeLine(params ulong[] tiles) {
			foreach (var tile in tiles) {
				currentDataNode.InnerText += tile + ",";
			}
			currentDataNode.InnerText += "\n";
		}

		void fixCurrentDataEndLine() {
			currentDataNode.InnerText = currentDataNode.InnerText.Substring(0, currentDataNode.InnerText.Length - 2) + "\n";
		}		
		
		
		XmlNode createAndAppendLayerReturnDataNode(string name, string width, string height, string visible) {
			XmlNode layer = doc.CreateElement("layer");
			map.AppendChild(layer);
			layer.Attributes.Append(createAttribut(doc,"name",name));
			layer.Attributes.Append(createAttribut(doc,"width",width));
			layer.Attributes.Append(createAttribut(doc,"height",height));
			layer.Attributes.Append(createAttribut(doc,"visible",visible));
			
			XmlNode data = doc.CreateElement("data");
			layer.AppendChild(data);
			data.Attributes.Append(createAttribut(doc,"encoding","csv"));
			data.InnerText = "\n";
			
			return data;
		}	
		
		protected XmlAttribute createAttribut(XmlDocument doc, string name, string value){
			XmlAttribute attribute = doc.CreateAttribute(name);
			attribute.Value = value;
			return attribute;
		}
	}
	
}
#endif