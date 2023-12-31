using System;

// Token: 0x0200036C RID: 876
internal class AATree<TKey, TValue> where TKey : IComparable<TKey>
{
	// Token: 0x06001C8C RID: 7308 RVA: 0x000FD30C File Offset: 0x000FB50C
	public AATree()
	{
		this.root = (this.sentinel = new AATree<TKey, TValue>.Node());
		this.deleted = null;
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x000FD33C File Offset: 0x000FB53C
	private void Skew(ref AATree<TKey, TValue>.Node node)
	{
		if (node.level == node.left.level)
		{
			AATree<TKey, TValue>.Node left = node.left;
			node.left = left.right;
			left.right = node;
			node = left;
		}
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x000FD384 File Offset: 0x000FB584
	private void Split(ref AATree<TKey, TValue>.Node node)
	{
		if (node.right.right.level == node.level)
		{
			AATree<TKey, TValue>.Node right = node.right;
			node.right = right.left;
			right.left = node;
			node = right;
			node.level++;
		}
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x000FD3E0 File Offset: 0x000FB5E0
	private bool Insert(ref AATree<TKey, TValue>.Node node, TKey key, TValue value)
	{
		if (node == this.sentinel)
		{
			node = new AATree<TKey, TValue>.Node(key, value, this.sentinel);
			return true;
		}
		int num = key.CompareTo(node.key);
		if (num < 0)
		{
			if (!this.Insert(ref node.left, key, value))
			{
				return false;
			}
		}
		else
		{
			if (num <= 0)
			{
				return false;
			}
			if (!this.Insert(ref node.right, key, value))
			{
				return false;
			}
		}
		this.Skew(ref node);
		this.Split(ref node);
		return true;
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x000FD478 File Offset: 0x000FB678
	private bool Delete(ref AATree<TKey, TValue>.Node node, TKey key)
	{
		if (node == this.sentinel)
		{
			return this.deleted != null;
		}
		int num = key.CompareTo(node.key);
		if (num < 0)
		{
			if (!this.Delete(ref node.left, key))
			{
				return false;
			}
		}
		else
		{
			if (num == 0)
			{
				this.deleted = node;
			}
			if (!this.Delete(ref node.right, key))
			{
				return false;
			}
		}
		if (this.deleted != null)
		{
			this.deleted.key = node.key;
			this.deleted.value = node.value;
			this.deleted = null;
			node = node.right;
		}
		else if (node.left.level < node.level - 1 || node.right.level < node.level - 1)
		{
			node.level--;
			if (node.right.level > node.level)
			{
				node.right.level = node.level;
			}
			this.Skew(ref node);
			this.Skew(ref node.right);
			this.Skew(ref node.right.right);
			this.Split(ref node);
			this.Split(ref node.right);
		}
		return true;
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x000FD5E8 File Offset: 0x000FB7E8
	private AATree<TKey, TValue>.Node Search(AATree<TKey, TValue>.Node node, TKey key)
	{
		if (node == this.sentinel)
		{
			return null;
		}
		int num = key.CompareTo(node.key);
		if (num < 0)
		{
			return this.Search(node.left, key);
		}
		if (num > 0)
		{
			return this.Search(node.right, key);
		}
		return node;
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x000FD644 File Offset: 0x000FB844
	public bool Add(TKey key, TValue value)
	{
		return this.Insert(ref this.root, key, value);
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x000FD654 File Offset: 0x000FB854
	public bool Remove(TKey key)
	{
		return this.Delete(ref this.root, key);
	}

	// Token: 0x17000833 RID: 2099
	public TValue this[TKey key]
	{
		get
		{
			AATree<TKey, TValue>.Node node = this.Search(this.root, key);
			return (node != null) ? node.value : default(TValue);
		}
		set
		{
			AATree<TKey, TValue>.Node node = this.Search(this.root, key);
			if (node == null)
			{
				this.Add(key, value);
			}
			else
			{
				node.value = value;
			}
		}
	}

	// Token: 0x04002167 RID: 8551
	private AATree<TKey, TValue>.Node root;

	// Token: 0x04002168 RID: 8552
	private AATree<TKey, TValue>.Node sentinel;

	// Token: 0x04002169 RID: 8553
	private AATree<TKey, TValue>.Node deleted;

	// Token: 0x0200036D RID: 877
	private class Node
	{
		// Token: 0x06001C96 RID: 7318 RVA: 0x000FD6D4 File Offset: 0x000FB8D4
		internal Node()
		{
			this.level = 0;
			this.left = this;
			this.right = this;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000FD6F4 File Offset: 0x000FB8F4
		internal Node(TKey key, TValue value, AATree<TKey, TValue>.Node sentinel)
		{
			this.level = 1;
			this.left = sentinel;
			this.right = sentinel;
			this.key = key;
			this.value = value;
		}

		// Token: 0x0400216A RID: 8554
		internal int level;

		// Token: 0x0400216B RID: 8555
		internal AATree<TKey, TValue>.Node left;

		// Token: 0x0400216C RID: 8556
		internal AATree<TKey, TValue>.Node right;

		// Token: 0x0400216D RID: 8557
		internal TKey key;

		// Token: 0x0400216E RID: 8558
		internal TValue value;
	}
}
