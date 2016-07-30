﻿

//Generation Time: 7/24/2016 8:10:46 PM
//This class was automatically generated by a T4 template.
//Making manual changes in this class might be overridden if the template will be processed again.
//If you want to add functionality you might be able to do this via another partial class definition for this class.

using System;
using AGS.API;
using AGS.Engine;
using System.Threading.Tasks;
using System.Collections.Generic;
using Autofac;

namespace AGS.Engine
{
    public partial class AGSCharacter : AGSEntity, ICharacter
    {
        private IHasRoom _hasRoom;
        private IAnimationContainer _animationContainer;
        private IInObjectTree _inObjectTree;
        private ICollider _collider;
        private IVisibleComponent _visibleComponent;
        private IEnabledComponent _enabledComponent;
        private ICustomProperties _customProperties;
        private IDrawableInfo _drawableInfo;
        private IHotspotComponent _hotspotComponent;
        private IShaderComponent _shaderComponent;
        private ISayBehavior _sayBehavior;
        private IWalkBehavior _walkBehavior;
        private IFaceDirectionBehavior _faceDirectionBehavior;
        private IHasOutfit _hasOutfit;
        private IHasInventory _hasInventory;
        private IFollowBehavior _followBehavior;

        public AGSCharacter(string id, Resolver resolver, IOutfit outfit) : base(id, resolver)
        {            
            _hasRoom = AddComponent<IHasRoom>();            
            _animationContainer = AddComponent<IAnimationContainer>();            
            _inObjectTree = AddComponent<IInObjectTree>();            
            _collider = AddComponent<ICollider>();            
            _visibleComponent = AddComponent<IVisibleComponent>();            
            _enabledComponent = AddComponent<IEnabledComponent>();            
            _customProperties = AddComponent<ICustomProperties>();            
            _drawableInfo = AddComponent<IDrawableInfo>();            
            _hotspotComponent = AddComponent<IHotspotComponent>();            
            _shaderComponent = AddComponent<IShaderComponent>();            
            _faceDirectionBehavior = AddComponent<IFaceDirectionBehavior>();            
            _hasOutfit = AddComponent<IHasOutfit>();            
            _hasInventory = AddComponent<IHasInventory>();            
            _followBehavior = AddComponent<IFollowBehavior>();
            init(resolver, outfit);
            InitComponents();
        }

        public string Name { get { return ID; } }
        public bool AllowMultiple { get { return false; } }
        public void Init(IEntity entity) {}

        public override string ToString()
        {
            return string.Format("{0} ({1})", ID ?? "", GetType().Name);
        }

        partial void init(Resolver resolver, IOutfit outfit);

        #region IHasRoom implementation

        public IRoom Room 
        {  
            get { return _hasRoom.Room; } 
        }

        public IRoom PreviousRoom 
        {  
            get { return _hasRoom.PreviousRoom; } 
        }

        #endregion

        #region IAnimationContainer implementation

        public IAnimation Animation 
        {  
            get { return _animationContainer.Animation; } 
        }

        public Boolean DebugDrawAnchor 
        {  
            get { return _animationContainer.DebugDrawAnchor; }  
            set { _animationContainer.DebugDrawAnchor = value; } 
        }

        public IBorderStyle Border 
        {  
            get { return _animationContainer.Border; }  
            set { _animationContainer.Border = value; } 
        }

        public void StartAnimation(IAnimation animation)
        {
            _animationContainer.StartAnimation(animation);
        }

        public AnimationCompletedEventArgs Animate(IAnimation animation)
        {
            return _animationContainer.Animate(animation);
        }

        public Task<AnimationCompletedEventArgs> AnimateAsync(IAnimation animation)
        {
            return _animationContainer.AnimateAsync(animation);
        }

        public void ResetScale(Single initialWidth, Single initialHeight)
        {
            _animationContainer.ResetScale(initialWidth, initialHeight);
        }

        #endregion

        #region ISprite implementation

        public Single X 
        {  
            get { return _animationContainer.X; }  
            set { _animationContainer.X = value; } 
        }

        public Single Y 
        {  
            get { return _animationContainer.Y; }  
            set { _animationContainer.Y = value; } 
        }

        public Single Z 
        {  
            get { return _animationContainer.Z; }  
            set { _animationContainer.Z = value; } 
        }

        public IArea PixelPerfectHitTestArea 
        {  
            get { return _animationContainer.PixelPerfectHitTestArea; } 
        }

        public Single Height 
        {  
            get { return _animationContainer.Height; } 
        }

        public Single Width 
        {  
            get { return _animationContainer.Width; } 
        }

        public Single ScaleX 
        {  
            get { return _animationContainer.ScaleX; } 
        }

        public Single ScaleY 
        {  
            get { return _animationContainer.ScaleY; } 
        }

        public Single Angle 
        {  
            get { return _animationContainer.Angle; }  
            set { _animationContainer.Angle = value; } 
        }

        public Byte Opacity 
        {  
            get { return _animationContainer.Opacity; }  
            set { _animationContainer.Opacity = value; } 
        }

        public Color Tint 
        {  
            get { return _animationContainer.Tint; }  
            set { _animationContainer.Tint = value; } 
        }

        public PointF Anchor 
        {  
            get { return _animationContainer.Anchor; }  
            set { _animationContainer.Anchor = value; } 
        }

        public IImage Image 
        {  
            get { return _animationContainer.Image; }  
            set { _animationContainer.Image = value; } 
        }

        public IImageRenderer CustomRenderer 
        {  
            get { return _animationContainer.CustomRenderer; }  
            set { _animationContainer.CustomRenderer = value; } 
        }

        public void PixelPerfect(Boolean pixelPerfect)
        {
            _animationContainer.PixelPerfect(pixelPerfect);
        }

        public void ResetScale()
        {
            _animationContainer.ResetScale();
        }

        public void ScaleBy(Single scaleX, Single scaleY)
        {
            _animationContainer.ScaleBy(scaleX, scaleY);
        }

        public void ScaleTo(Single width, Single height)
        {
            _animationContainer.ScaleTo(width, height);
        }

        public void FlipHorizontally()
        {
            _animationContainer.FlipHorizontally();
        }

        public void FlipVertically()
        {
            _animationContainer.FlipVertically();
        }

        public ISprite Clone()
        {
            return _animationContainer.Clone();
        }

        #endregion

        #region IInObjectTree implementation

        #endregion

        #region IInTree<IObject> implementation

        public ITreeNode<IObject> TreeNode 
        {  
            get { return _inObjectTree.TreeNode; } 
        }

        #endregion

        #region ICollider implementation

        public ISquare BoundingBox 
        {  
            get { return _collider.BoundingBox; }  
            set { _collider.BoundingBox = value; } 
        }

        public Nullable<PointF> CenterPoint 
        {  
            get { return _collider.CenterPoint; } 
        }

        public Boolean CollidesWith(Single x, Single y)
        {
            return _collider.CollidesWith(x, y);
        }

        #endregion

        #region IVisibleComponent implementation

        public Boolean Visible 
        {  
            get { return _visibleComponent.Visible; }  
            set { _visibleComponent.Visible = value; } 
        }

        public Boolean UnderlyingVisible 
        {  
            get { return _visibleComponent.UnderlyingVisible; } 
        }

        #endregion

        #region IEnabledComponent implementation

        public Boolean Enabled 
        {  
            get { return _enabledComponent.Enabled; }  
            set { _enabledComponent.Enabled = value; } 
        }

        public Boolean UnderlyingEnabled 
        {  
            get { return _enabledComponent.UnderlyingEnabled; } 
        }

        #endregion

        #region ICustomProperties implementation

        public Int32 GetInt(String name, Int32 defaultValue)
        {
            return _customProperties.GetInt(name, defaultValue);
        }

        public void SetInt(String name, Int32 value)
        {
            _customProperties.SetInt(name, value);
        }

        public Single GetFloat(String name, Single defaultValue)
        {
            return _customProperties.GetFloat(name, defaultValue);
        }

        public void SetFloat(String name, Single value)
        {
            _customProperties.SetFloat(name, value);
        }

        public String GetString(String name, String defaultValue)
        {
            return _customProperties.GetString(name, defaultValue);
        }

        public void SetString(String name, String value)
        {
            _customProperties.SetString(name, value);
        }

        public Boolean GetBool(String name, Boolean defaultValue)
        {
            return _customProperties.GetBool(name, defaultValue);
        }

        public void SetBool(String name, Boolean value)
        {
            _customProperties.SetBool(name, value);
        }

        public IDictionary<String,Int32> AllInts()
        {
            return _customProperties.AllInts();
        }

        public IDictionary<String,Single> AllFloats()
        {
            return _customProperties.AllFloats();
        }

        public IDictionary<String,String> AllStrings()
        {
            return _customProperties.AllStrings();
        }

        public IDictionary<String,Boolean> AllBooleans()
        {
            return _customProperties.AllBooleans();
        }

        public void RegisterCustomData(ICustomSerializable customData)
        {
            _customProperties.RegisterCustomData(customData);
        }

        public void CopyFrom(ICustomProperties properties)
        {
            _customProperties.CopyFrom(properties);
        }

        #endregion

        #region IDrawableInfo implementation

        public IRenderLayer RenderLayer 
        {  
            get { return _drawableInfo.RenderLayer; }  
            set { _drawableInfo.RenderLayer = value; } 
        }

        public Boolean IgnoreViewport 
        {  
            get { return _drawableInfo.IgnoreViewport; }  
            set { _drawableInfo.IgnoreViewport = value; } 
        }

        public Boolean IgnoreScalingArea 
        {  
            get { return _drawableInfo.IgnoreScalingArea; }  
            set { _drawableInfo.IgnoreScalingArea = value; } 
        }

        #endregion

        #region IHotspotComponent implementation

        public IInteractions Interactions 
        {  
            get { return _hotspotComponent.Interactions; } 
        }

        public Nullable<PointF> WalkPoint 
        {  
            get { return _hotspotComponent.WalkPoint; }  
            set { _hotspotComponent.WalkPoint = value; } 
        }

        public String Hotspot 
        {  
            get { return _hotspotComponent.Hotspot; }  
            set { _hotspotComponent.Hotspot = value; } 
        }

        #endregion

        #region IShaderComponent implementation

        public IShader Shader 
        {  
            get { return _shaderComponent.Shader; }  
            set { _shaderComponent.Shader = value; } 
        }

        #endregion

        #region ISayBehavior implementation

        public ISayConfig SpeechConfig 
        {  
            get { return _sayBehavior.SpeechConfig; } 
        }

        public IBlockingEvent<BeforeSayEventArgs> OnBeforeSay 
        {  
            get { return _sayBehavior.OnBeforeSay; } 
        }

        public void Say(String text)
        {
            _sayBehavior.Say(text);
        }

        public Task SayAsync(String text)
        {
            return _sayBehavior.SayAsync(text);
        }

        #endregion

        #region IWalkBehavior implementation

        public Single WalkSpeed 
        {  
            get { return _walkBehavior.WalkSpeed; }  
            set { _walkBehavior.WalkSpeed = value; } 
        }

        public Boolean AdjustWalkSpeedToScaleArea 
        {  
            get { return _walkBehavior.AdjustWalkSpeedToScaleArea; }  
            set { _walkBehavior.AdjustWalkSpeedToScaleArea = value; } 
        }

        public Boolean IsWalking 
        {  
            get { return _walkBehavior.IsWalking; } 
        }

        public Boolean DebugDrawWalkPath 
        {  
            get { return _walkBehavior.DebugDrawWalkPath; }  
            set { _walkBehavior.DebugDrawWalkPath = value; } 
        }

        public Boolean Walk(ILocation location)
        {
            return _walkBehavior.Walk(location);
        }

        public Task<Boolean> WalkAsync(ILocation location)
        {
            return _walkBehavior.WalkAsync(location);
        }

        public void StopWalking()
        {
            _walkBehavior.StopWalking();
        }

        public Task StopWalkingAsync()
        {
            return _walkBehavior.StopWalkingAsync();
        }

        public void PlaceOnWalkableArea()
        {
            _walkBehavior.PlaceOnWalkableArea();
        }

        #endregion

        #region IFaceDirectionBehavior implementation

        public Direction Direction 
        {  
            get { return _faceDirectionBehavior.Direction; } 
        }

        public IDirectionalAnimation CurrentDirectionalAnimation 
        {  
            get { return _faceDirectionBehavior.CurrentDirectionalAnimation; }  
            set { _faceDirectionBehavior.CurrentDirectionalAnimation = value; } 
        }

        public void FaceDirection(Direction direction)
        {
            _faceDirectionBehavior.FaceDirection(direction);
        }

        public Task FaceDirectionAsync(Direction direction)
        {
            return _faceDirectionBehavior.FaceDirectionAsync(direction);
        }

        public void FaceDirection(IObject obj)
        {
            _faceDirectionBehavior.FaceDirection(obj);
        }

        public Task FaceDirectionAsync(IObject obj)
        {
            return _faceDirectionBehavior.FaceDirectionAsync(obj);
        }

        public void FaceDirection(Single x, Single y)
        {
            _faceDirectionBehavior.FaceDirection(x, y);
        }

        public Task FaceDirectionAsync(Single x, Single y)
        {
            return _faceDirectionBehavior.FaceDirectionAsync(x, y);
        }

        public void FaceDirection(Single fromX, Single fromY, Single toX, Single toY)
        {
            _faceDirectionBehavior.FaceDirection(fromX, fromY, toX, toY);
        }

        public Task FaceDirectionAsync(Single fromX, Single fromY, Single toX, Single toY)
        {
            return _faceDirectionBehavior.FaceDirectionAsync(fromX, fromY, toX, toY);
        }

        #endregion

        #region IHasOutfit implementation

        public IOutfit Outfit 
        {  
            get { return _hasOutfit.Outfit; }  
            set { _hasOutfit.Outfit = value; } 
        }

        #endregion

        #region IHasInventory implementation

        public IInventory Inventory 
        {  
            get { return _hasInventory.Inventory; }  
            set { _hasInventory.Inventory = value; } 
        }

        #endregion

        #region IFollowBehavior implementation

        public void Follow(IObject obj, IFollowSettings settings)
        {
            _followBehavior.Follow(obj, settings);
        }

        #endregion
    }
}

