using Library.Models;
using System;
using System.Security.Cryptography.X509Certificates;

namespace WpfUI.ViewModels;

public class TileViewModel 
    : ViewModelBase<TileViewModel>
    , IEquatable<TileModel>
{
    public TileModel TileModel { get; }
    public int Row => TileModel.yPos;
    public int Column => TileModel.xPos;

    public TileViewModel(TileModel tileModel)
        : base()
    {
        TileModel = tileModel ?? throw new ArgumentNullException(nameof(tileModel));
    }


    public bool Equals(TileModel? other)
    {
        return TileModel.Equals(other);
    }
}
